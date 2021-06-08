using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ParcelTracker.Models.Database;
using ParcelTracker.Models.Response;
using ParcelTracker.Options;

namespace ParcelTracker.Controllers
{
    [ApiController]
    [Route("api/parcels")]
    public class ParcelController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ParcelDbContext dbContext;
        private readonly IDistributedCache distributedCache;
        private readonly AppOptions appOptions;

        public ParcelController(IMapper mapper, ParcelDbContext dbContext, AppOptions appOptions, IDistributedCache distributedCache = null)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.distributedCache = distributedCache;
            this.appOptions = appOptions;
        }

        // public ParcelController(IMapper mapper, ParcelDbContext dbContext, AppOptions appOptions) : this(mapper, dbContext, appOptions, null) { }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetParcel([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            byte[] serializedData = null;
            if (appOptions.UseCache)
                serializedData = await distributedCache.GetAsync(CacheKeys.GetParcelKey(id), cancellationToken);
            if (serializedData != null)
                return Ok(JsonSerializer.Deserialize<ParcelResponseModel>(serializedData));
            ParcelResponseModel parcel = await GetParcelAsync(id, cancellationToken);
            if (parcel == null)
                return NotFound();
            if (appOptions.UseCache)
                await SaveParcelInCache(parcel, cancellationToken);
            return Ok(parcel);
        }

        [HttpPost("{id:guid}/state/{stateId:int}")]
        public async Task<IActionResult> AddState([FromRoute] Guid id, int stateId, CancellationToken cancellationToken)
        {
            ParcelState parcelState = new()
            {
                ParcelId = id,
                StateId = stateId,
                Created = DateTimeOffset.Now
            };
            dbContext.Add(parcelState);
            await dbContext.SaveChangesAsync(cancellationToken);
            ParcelResponseModel parcel = await GetParcelAsync(id, cancellationToken);
            if (appOptions.UseCache)
                await SaveParcelInCache(parcel, cancellationToken);
            return Ok(parcel);
        }

        private Task<ParcelResponseModel> GetParcelAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return dbContext.Set<Parcel>().AsNoTracking().ProjectTo<ParcelResponseModel>(mapper.ConfigurationProvider).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        private Task SaveParcelInCache(ParcelResponseModel parcel, CancellationToken cancellationToken = default)
        {
            return distributedCache.SetAsync(CacheKeys.GetParcelKey(parcel.Id), JsonSerializer.SerializeToUtf8Bytes(parcel), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now + appOptions.Parcels.AbsoluteExpiration,
                SlidingExpiration = appOptions.Parcels.SlidingExpiration
            }, cancellationToken);
        }
    }
}
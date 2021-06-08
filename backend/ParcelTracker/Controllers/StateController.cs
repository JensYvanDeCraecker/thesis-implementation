using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParcelTracker.Models.Database;
using ParcelTracker.Models.Response;
using ParcelTracker.Options;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ParcelTracker.Controllers
{
    [ApiController]
    [Route("api/states")]
    public class StateController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ParcelDbContext dbContext;
        private readonly IDistributedCache distributedCache;
        private readonly AppOptions appOptions;

        public StateController(IMapper mapper, ParcelDbContext dbContext, AppOptions appOptions, IDistributedCache distributedCache = null)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.distributedCache = distributedCache;
            this.appOptions = appOptions;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStates(CancellationToken cancellationToken)
        {
            byte[] cachedData = null;
            if (appOptions.UseCache)
                cachedData = await distributedCache.GetAsync(CacheKeys.AllStates, cancellationToken);
            if (cachedData != null)
                return Ok(JsonSerializer.Deserialize<IEnumerable<StateResponseModel>>(cachedData));
            List<StateResponseModel> states = await GetStatesAsync(cancellationToken);
            if (appOptions.UseCache)
                await distributedCache.SetAsync(CacheKeys.AllStates, JsonSerializer.SerializeToUtf8Bytes(states), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now + appOptions.AllStates.AbsoluteExpiration,
                    SlidingExpiration = appOptions.AllStates.SlidingExpiration
                }, cancellationToken);
            return Ok(states);
        }

        private Task<List<StateResponseModel>> GetStatesAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.Set<State>().AsNoTracking().ProjectTo<StateResponseModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
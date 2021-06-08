using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParcelTracker.Models.Database;

namespace ParcelTracker.Controllers
{
    [ApiController]
    [Route("api/generate")]
    public class GenerateController : ControllerBase
    {
        private readonly ParcelDbContext dbContext;
        private readonly ILogger<GenerateController> logger;

        public GenerateController(ParcelDbContext dbContext, ILogger<GenerateController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpPost("{amount:int}")]
        public async Task<IActionResult> GenerateParcels([FromRoute] int amount, CancellationToken cancellationToken)
        {
            Faker<Address> addressFaker = new Faker<Address>("nl_BE")
                .RuleFor(a => a.Name, f => f.Person.FullName)
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.HouseNumber, f => f.Address.BuildingNumber())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Country, _ => "België");
            List<Parcel> generatedParcels = new(amount);
            DateTimeOffset current = DateTimeOffset.Now;
            for (int i = 0; i < amount; i++)
            {
                Parcel parcel = new()
                {
                    ReturnAddress = addressFaker.Generate(),
                    DeliveryAddress = addressFaker.Generate(),
                    ParcelStates = new List<ParcelState>()
                    {
                        new()
                        {
                            StateId = 1,
                            Created = current
                        }
                    }
                };
                generatedParcels.Add(parcel);
            }
            await dbContext.AddRangeAsync(generatedParcels, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Ok(generatedParcels.Select(p => p.Id).ToList());
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ParcelTracker
{
    public class MigrateHostedService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public MigrateHostedService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);
            using IServiceScope scope = scopeFactory.CreateScope();
            ParcelDbContext dbContext = scope.ServiceProvider.GetRequiredService<ParcelDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken);
          
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Seed.RequiredSeeds;
using System;
using System.Threading.Tasks;

namespace Persistence
{
    public class SeedData
    {
        public static async Task InitializeIfEmptyAsync(IServiceProvider serviceProvider)
        {
            using (var dbContext = new RsDbContext(serviceProvider.GetRequiredService<DbContextOptions<RsDbContext>>()))
            {
                await CounterSeed.SeedCountersIfEmpty(dbContext);
            }
        }

        internal static void AddSampleData(IServiceProvider serviceProvider)
        {
            return;
        }
    }
}

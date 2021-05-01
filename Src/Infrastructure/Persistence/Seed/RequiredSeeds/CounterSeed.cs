using Domain.Enums;
using Domain.Models;
using System.Threading.Tasks;

namespace Persistence.Seed.RequiredSeeds
{
    public class CounterSeed
    {
        internal static async Task SeedCountersIfEmpty(RsDbContext dbContext)
        {
            if (await dbContext.Counters.FindAsync(CounterId.ButtonClicks) == null)
                dbContext.Counters.Add(new Counter() { CounterId = (int)CounterId.ButtonClicks, Count = 0 });
        }
    }
}

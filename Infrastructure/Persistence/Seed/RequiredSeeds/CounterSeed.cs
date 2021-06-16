using Domain.Constants;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistence.Seed.RequiredSeeds
{
    public class CounterSeed
    {
        internal static async Task SeedCountersIfEmpty(RsDbContext dbContext)
        {
            var buttonClicksCounter = await dbContext.Counters.FirstOrDefaultAsync(c => c.Name == CounterNames.ButtonClicks);
            if (buttonClicksCounter == null)
                dbContext.Counters.Add(new Counter() { Name = CounterNames.ButtonClicks, Count = 0 });
        }
    }
}

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRsDbContext
    {
        DbSet<Counter> Counters { get; set; }
        DbSet<Email> Emails { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Dispose();
    }
}

using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.TestUtilities
{
    public class RsTestDbContext : DbContext, IRsDbContext
    {
        public RsTestDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Counter> Counters { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}

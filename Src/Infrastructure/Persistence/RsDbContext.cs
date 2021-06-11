using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence
{
    public class RsDbContext : DbContext, IRsDbContext
    {
        public RsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<IpAddress> IpAddresses { get; set; }

        protected internal void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

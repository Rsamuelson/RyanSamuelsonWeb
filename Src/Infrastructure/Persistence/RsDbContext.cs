using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class RsDbContext : DbContext, IRsDbContext
    {
        public DbSet<Email> Emails { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}

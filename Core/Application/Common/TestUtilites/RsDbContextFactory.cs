using Microsoft.EntityFrameworkCore;
using System;

namespace Application.Common.TestUtilities
{
    public static class RsDbContextFactory
    {
        public static void Destory(RsTestDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }

        public static RsTestDbContext Create()
        {
            var options = new DbContextOptionsBuilder<RsTestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new RsTestDbContext(options);

            context.Database.EnsureCreated();

            context.SaveChanges();

            return context;
        }
    }
}

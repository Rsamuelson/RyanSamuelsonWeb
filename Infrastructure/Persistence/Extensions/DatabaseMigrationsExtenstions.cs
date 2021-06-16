using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public class DatabaseMigrationsExtenstions
    {
        public static async Task RunDatebaseMigrationsAsync(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            var logger = loggerFactory.CreateLogger<DatabaseMigrationsExtenstions>();

            try
            {
                var context = serviceProvider.GetRequiredService<RsDbContext>();
                var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();

                if (hostEnvironment.IsDevelopment())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
                else
                {
                    logger.LogInformation("Beginning database migration.");
                    context.Database.Migrate();
                    logger.LogInformation("Migrated database successfully.");
                }

                await SeedData.InitializeIfEmptyAsync(serviceProvider);

                if (hostEnvironment.IsDevelopment())
                {
                    SeedData.AddSampleData(serviceProvider);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrating the database");

                throw;
            }
        }
    }
}

using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseGuid = Guid.NewGuid().ToString();
            services.AddDbContext<RsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                    .Replace("%CONTENTROOTPATH%", Environment.CurrentDirectory)
                    .Replace("%GUID%", databaseGuid),
                    cfg => cfg.MigrationsAssembly(typeof(RsDbContext).Assembly.GetName().Name)
                ));

            services.AddScoped<IRsDbContext>(provider => provider.GetService<RsDbContext>());

            return services;
        }
    }
}

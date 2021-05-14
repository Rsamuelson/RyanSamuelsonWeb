using Infrastructure.Logging.Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = SerilogUtilities.GetLogger();
            try
            {
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                    //await DatabaseMigrationsExtenstions.RunDatebaseMigrationsAsync(loggerFactory, serviceProvider);
                }

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Host terminated unexpectedly.");

                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog();
    }
}

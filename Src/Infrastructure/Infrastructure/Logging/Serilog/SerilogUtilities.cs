using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace Infrastructure.Logging.Serilog
{
    public class SerilogUtilities
    {
        private static readonly string AspNetEnviornment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static ILogger GetLogger()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{AspNetEnviornment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configurationBuilder)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .CreateLogger();
        }
    }
}

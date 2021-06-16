using Application.Emails.Interfaces;
using Common;
using Infrastructure.Email;
using Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, SendGridEmailService>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}

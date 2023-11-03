using Microsoft.Extensions.Options;
using Monitoring.Postgresql.Settings;

namespace Monitoring.Postgresql.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection AddConfigOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string section)
               where TOptions : class, new()
        {
            services.Configure<TOptions>(configuration.GetSection(section));
            services.AddSingleton(serviceProvider => serviceProvider.GetService<IOptions<TOptions>>()?.Value);

            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigOptions<JwtSettings>(configuration, nameof(JwtSettings));
            return services;
        }
    }
}

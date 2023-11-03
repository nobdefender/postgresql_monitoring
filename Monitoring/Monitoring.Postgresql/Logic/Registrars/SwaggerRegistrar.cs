using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Monitoring.Postgresql.Logic.Registrars;

public static class SwaggerRegistrar
{
    /// <summary>
    /// Регистрирует настройки swagger.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов сервисов.</param>
    public static void RegisterSwagger(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }
}
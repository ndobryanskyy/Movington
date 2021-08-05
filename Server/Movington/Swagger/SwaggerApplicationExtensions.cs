using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Movington.Swagger
{
    internal static class SwaggerApplicationExtensions
    {
        public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
        {
            services
                .AddOptions<SwaggerApplicationOptions>()
                .BindConfiguration(SwaggerApplicationOptions.SectionName);

            return services
                .AddSwaggerGen()
                .ConfigureOptions<SwaggerOptionsConfigurator>();
        }

        public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI();
        }
    }
}
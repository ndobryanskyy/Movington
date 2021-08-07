using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Movington.Database
{
    internal static class DatabaseApplicationExtensions
    {
        public static IServiceCollection AddConfiguredDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<NpgsqlApplicationDbContext>(options =>
                {
                    options
                        .UseLoggerFactory(LoggerFactory.Create(logger => logger.AddConsole()))
                        .UseNpgsql(configuration.GetConnectionString("Default"));
                })
                .AddScoped<ApplicationDbContext>(container => container.GetRequiredService<NpgsqlApplicationDbContext>());
        }
    }
}
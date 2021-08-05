using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Movington.Security
{
    internal static class SecurityApplicationExtensions
    {
        private const string AuthenticationSectionName = "Authentication";
        private const string CorsDefaultPolicySection = "Cors:DefaultPolicy";

        public static IServiceCollection AddConfiguredSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions => configuration.Bind(AuthenticationSectionName, jwtBearerOptions));

            return services
                .AddCors(options =>
                {
                    var policyOptions = new CorsPolicy();
                    configuration.Bind(CorsDefaultPolicySection, policyOptions);

                    options.AddDefaultPolicy(policyOptions);
                });
        }

        public static IApplicationBuilder UseConfiguredSecurity(this IApplicationBuilder app)
        {
            return app
                .UseCors()
                .UseAuthentication()
                .UseAuthorization();
        }
    }
}
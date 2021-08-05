using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Movington.Swagger
{
    internal sealed class SwaggerOptionsConfigurator : IConfigureOptions<SwaggerGenOptions>, IConfigureOptions<SwaggerUIOptions>
    {
        private readonly SwaggerApplicationOptions _options;

        public SwaggerOptionsConfigurator(IOptions<SwaggerApplicationOptions> swaggerOptions)
        {
            _options = swaggerOptions.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Movington",
                Version = "v1"
            });

            const string securitySchemeName = "auth0";

            options.AddSecurityDefinition(securitySchemeName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = _options.Security.OpenIdConnectUrl
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = securitySchemeName
                        }
                    },
                    new List<string> { "openid" }
                }
            });
        }

        public void Configure(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Movington v1");

            options.OAuthAppName("Movington.Swagger");
            options.OAuthClientId(_options.Security.ClientId);
            options.OAuthUsePkce();
            options.OAuthScopes("openid");
            options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
            {
                { "audience", _options.Security.Audience }
            });
        }
    }
}
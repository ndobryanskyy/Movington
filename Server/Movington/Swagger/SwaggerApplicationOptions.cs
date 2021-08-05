using System;

namespace Movington.Swagger
{
    internal sealed class SwaggerApplicationOptions
    {
        public const string SectionName = "Swagger";

        public SecurityOptions Security { get; set; } = new ();

        internal sealed class SecurityOptions
        {
            public Uri OpenIdConnectUrl { get; set; }

            public string ClientId { get; set; }

            public string Audience { get; set; }
        }
    }
}
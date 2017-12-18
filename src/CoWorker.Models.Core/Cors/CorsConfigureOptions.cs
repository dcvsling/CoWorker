using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;
using CoWorker.Primitives;

namespace CoWorker.Models.Core.Cors
{

    public class CorsConfigureOptions : IConfigureNamedOptions<CorsOptions>
    {
        private readonly IConfiguration _config;
        private readonly IOptionsSnapshot<CorsPolicyOptions> _snapshot;

        public CorsConfigureOptions(IConfiguration config,IOptionsSnapshot<CorsPolicyOptions> snapshot)
        {
            _config = config;
            _snapshot = snapshot;
        }
        public void Configure(string name, CorsOptions options)
        {
            _config.GetSection("cors")
                .GetChildren()
                .Select(x => _snapshot.Get(x.Value))
                .Each(x => options.AddPolicy(
                    x.Name,
                    builder => (x.Credentials
                            ? builder.AllowCredentials()
                            : builder.DisallowCredentials())
                        .WithExposedHeaders(x.ExposedHeaders.ToArray())
                        .WithHeaders(x.Headers.ToArray())
                        .WithMethods(x.Methods.ToArray())
                        .WithMethods(x.Methods.ToArray())
                        .SetPreflightMaxAge(new TimeSpan(x.MaxAge))
                        .SetIsOriginAllowedToAllowWildcardSubdomains()));
        }

        public void Configure(CorsOptions options)
            => Configure(string.Empty, options);
    }
}

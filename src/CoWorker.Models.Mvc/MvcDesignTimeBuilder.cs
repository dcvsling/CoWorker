using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using CoWorker.Primitives;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Collections.Generic;
namespace CoWorker.Models.Mvc
{

    public class MvcDesignTimeBuilder : IDesignTimeMvcCoreBuilderConfiguration
    {
        private readonly IEnumerable<IApplicationFeatureProvider> _providers;
        private readonly IEnumerable<ApplicationPart> _parts;

        public MvcDesignTimeBuilder(
            IEnumerable<ApplicationPart> parts,
            IEnumerable<IApplicationFeatureProvider> providers)
        {
            _providers = providers;
            _parts = parts;
        }

        public void ConfigureMvc(IMvcCoreBuilder builder)
        {
            builder
                .AddControllersAsServices()
                .AddApiExplorer()
                .AddJsonFormatters(o => o.Initialize())
                .AddJsonOptions(o => o.SerializerSettings.Initialize())
                .ConfigureApplicationPartManager(part => _providers.Each(part.FeatureProviders.Add))
                .ConfigureApplicationPartManager(part => _parts.Each(part.ApplicationParts.Add))
                .Services.AddSingleton<IConfigureOptions<IRouteBuilder>,RouteBuilderConfigureOptions>();
        }
    }
}

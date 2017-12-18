using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using CoWorker.Primitives;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace CoWorker.Models.Mvc
{

    public class LazyApplicationPartManager : ApplicationPartManager
    {
        private readonly ApplicationPartManager _manager;

        public LazyApplicationPartManager(IOptions<ApplicationPartManager> manager)
        {
            _manager = manager.Value;
        }
        public new IList<IApplicationFeatureProvider> FeatureProviders => _manager.FeatureProviders;
        public new IList<ApplicationPart> ApplicationParts => _manager.ApplicationParts;


        public new void PopulateFeature<TFeature>(TFeature feature)
            => _manager.PopulateFeature(feature);
    }
}

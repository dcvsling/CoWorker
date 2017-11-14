using Microsoft.Extensions.Options;
using System.Linq;
using CoWorker.Primitives;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.ApplicationParts
{

    public class ApplicationFeatureFactory : IApplicationFeatureFactory
    {
        private readonly IEnumerable<ApplicationPart> _parts;
        private readonly IEnumerable<IApplicationFeatureProvider> _providers;
        private readonly IOptionsMonitorCache<Feature> _features;

        public ApplicationFeatureFactory(
            IEnumerable<ApplicationPart> parts,
            IEnumerable<IApplicationFeatureProvider> providers,
            IOptionsMonitorCache<Feature> features)
        {
            _parts = parts;
            _providers = providers;
            _features = features;
        }

        public Feature Create(string name)
            => _features.GetOrAdd(name, () => CreateFeature(name));

        private Feature CreateFeature(string name)
            => _providers.Where(x => x.Name == name)
                .Aggregate(new Feature(), (seed, next) => next.Create(_parts, seed));
    }
}

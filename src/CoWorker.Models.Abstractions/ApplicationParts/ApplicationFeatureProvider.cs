using System;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.ApplicationParts
{
    using CoWorker.Primitives;

    public class NamedApplicationFeatureProvider : IApplicationFeatureProvider
    {
        private readonly Func<ApplicationPart, IEnumerable<Type>> _provider;

        public NamedApplicationFeatureProvider(string name,Func<ApplicationPart,IEnumerable<Type>> provider)
        {
            _provider = provider;
            Name = name;
        }

        public string Name { get; }

        public void Create(IEnumerable<ApplicationPart> parts,Feature feature)
        {
            parts.Aggregate(feature, GetAllowType);
        }

        private void GetAllowType(Feature feature, ApplicationPart part)
            => feature.Types.AddRange(_provider(part));
    }
}

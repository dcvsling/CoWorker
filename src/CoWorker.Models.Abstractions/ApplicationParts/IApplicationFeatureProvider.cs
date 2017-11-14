using CoWorker.Primitives;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.ApplicationParts
{
    public interface IApplicationFeatureProvider : IName
    {
        void Create(IEnumerable<ApplicationPart> parts, Feature feature);
    }
}
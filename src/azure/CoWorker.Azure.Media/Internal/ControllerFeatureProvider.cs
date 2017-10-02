using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Linq;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using CoWorker.Azure.Media.Controllers;

namespace CoWorker.Azure.Media.Internal
{

    public class ControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            parts.SelectMany(x => x is AssemblyPart assembly ? assembly.Types : Enumerable.Empty<Type>())
                .Where(x => (x.BaseType?.IsGenericType ?? false)
                    && typeof(BaseCollection<>).IsAssignableFrom(x.BaseType.GetGenericTypeDefinition()))
                .Select(x => typeof(AzureResourceController<>).MakeGenericType(x))
                .ToList()
                .ForEach(x => feature.Controllers.Add(x.GetTypeInfo()));
        }
    }
}

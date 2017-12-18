using CoWorker.Models.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using CoWorker.Primitives;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CoWorker.Models.Mvc
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var types = parts.SelectMany(x => x is AssemblyPart asm ? asm.Types : Type.EmptyTypes.Select(y => y.GetTypeInfo()));
            var models = parts.SelectMany(x => x.Name == "GenericControllerModel" && x is IApplicationPartTypeProvider provider ? provider.Types : Type.EmptyTypes.Select(y => y.GetTypeInfo()));
            types.Where(x => x.GetCustomAttributes<ControllerAttribute>(true).Any() && !x.IsAbstract && x.IsGenericType)
                .SelectMany(x => models.Select(y => x.MakeGenericType(y)))
                .Select(x => x.GetTypeInfo())
                .Each(feature.Controllers.Add);
        }

        public IEnumerable<Type> GetCloseGenericTypes(IEnumerable<Type> types,Type type)
            => types.SelectMany(y => type.GetGenericArguments().SelectMany(z => z.GetGenericParameterConstraints().Where(x => x.IsAssignableFrom(y))));
    }
}

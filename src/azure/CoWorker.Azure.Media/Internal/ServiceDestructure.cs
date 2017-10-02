using System.Linq.Expressions;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoWorker.Azure.Media.Internal
{

    public class ServiceDestructure
    {
        private readonly IServiceCollection _services;

        public ServiceDestructure(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceCollection Destructure<TService>() 
            => typeof(TService).GetProperties()
                .Where(x => !x.PropertyType.IsValueType || x.PropertyType == typeof(string))
                .Select(GetPropertyService)
                .Aggregate(_services, (seed, next) => seed.Add(next));

        private ServiceDescriptor GetPropertyService(PropertyInfo property)
        {
           return ServiceDescriptor.Transient(
                property.PropertyType,
                typeof(IServiceProvider)
                    .ToParameter()
                    .MakeLambda<Func<IServiceProvider, object>>(exp => exp
                         .MethodCall("GetService", property.DeclaringType.ToConstant())
                         .AsTypeTo(property.DeclaringType)
                         .GetPropertyOrField(property.Name)
                         .AsTypeTo(typeof(object)))
                    .Compile());
        }
    }
}

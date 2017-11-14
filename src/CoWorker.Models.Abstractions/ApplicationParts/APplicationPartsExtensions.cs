using System.Collections;
using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.ApplicationParts
{
    public static class ApplicationPartsExtensions
    {
        public static ApplicationPartsBuilder AddApplicationPart(this IServiceCollection services)
        {
            services.TryAddSingleton<IApplicationFeatureFactory,ApplicationFeatureFactory>();
            return new ApplicationPartsBuilder(services);
        }

        public static ApplicationPartsBuilder AddProvider<TProvider>(this ApplicationPartsBuilder builder)
            where TProvider : class, IApplicationFeatureProvider
        {
            builder.Services.AddSingleton<IApplicationFeatureProvider, TProvider>();
            return builder;
        }

        public static ApplicationPartsBuilder ConfigureFeature(this ApplicationPartsBuilder builder,string name,Func<ApplicationPart,IEnumerable<Type>> provider)
        {
            builder.Services.AddSingleton<IApplicationFeatureProvider>(p => new NamedApplicationFeatureProvider(name,provider));
            return builder;
        }
    }
}

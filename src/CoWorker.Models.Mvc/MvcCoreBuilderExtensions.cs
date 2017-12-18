using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using CoWorker.Primitives;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace CoWorker.Models.Mvc
{

    public static class MvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddMvcCoreDesignTime<TConfig>(this IMvcCoreBuilder builder)
            where TConfig : IDesignTimeMvcCoreBuilderConfiguration
        {
            builder.Services.TryAddScoped(typeof(MntController<>));
            builder.Services.AddSingleton<IConfigureOptions<MvcOptions>,MvcOptionsConfigureOptions>();
            builder.Services.TryAddSingleton<IApplicationModelConvention, GenericControllerModelConvention>();
            builder.Services.TryAddSingleton<IApplicationFeatureProvider, GenericControllerFeatureProvider>();
            builder.Services.Configure<ApplicationPartManager>(manager => builder.PartManager.ApplicationParts.Aggregate(manager, (seed,next) => seed.ApplicationParts.Add(next)));
            builder.Services.Configure<ApplicationPartManager>(manager => builder.PartManager.FeatureProviders.Aggregate(manager, (seed, next) => seed.FeatureProviders.Add(next)));
            builder.Services.TryAddSingleton<ApplicationPartManager, LazyApplicationPartManager>();
            return new CustomMvcCoreBuilder(builder).Configure();
        }
    }
}

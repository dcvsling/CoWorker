using Microsoft.Extensions.Internal;
namespace CoWorker.Builder
{
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.ValueGeneration;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using CoWorker.EntityFramework.Conventions;
    using CoWorker.EntityFramework.EntityParts.Conventions;
    using CoWorker.EntityFramework.ValueGenerators;


    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConventionSet<TConvetion>(this IServiceCollection services)
            where TConvetion : class, IConfigureOptions<ConventionSet>
        {
            services.TryAddSingleton<IConfigureOptions<ConventionSet>, TConvetion>();
            return services;
        }

        public static IServiceCollection AddDefaultModelBuilderConventions(this IServiceCollection services)
        {
            services.TryAddSingleton<ICoreConventionSetBuilder, CoreConventionSetBuilder>();
            services.TryAddSingleton<CoreConventionSetBuilderDependencies>();
            return services.AddConventionSet<AutoKeyConventions>()
                .AddConventionSet<StringMaxLengthConventions>()
                .AddConventionSet<FormatTableNameConvention>()
                .AddConventionSet<NeverNullConvention>()
                .AddConventionSet<ForceRemoveAllCascadeOnDeleteConvention>()
                .AddConventionSet<AutoLogDatetimeConventions>()
                .AddConventionSet<AutoRowLogConvention>();
        }
        public static IServiceCollection AddSharedUnit<TUnit>(this IServiceCollection services)
            => services.AddConventionSet<SharedUnitConvention<TUnit>>();
        
        public static IServiceCollection AddValueGeneratorProvider<TProvider>(
            this IServiceCollection services)
            where TProvider : ValueGenerator
        {
            services.TryAddSingleton<TProvider>();
            return services;
        }

        public static IServiceCollection AddDefaultValueGeneratorProviders(this IServiceCollection services)
        {
            services
                  .AddValueGeneratorProvider<DatetimeOffsetValueGenerator>()
                  .AddValueGeneratorProvider<DatetimeValueGenerator>()
                  .AddValueGeneratorProvider<CurrentUserValueGenerator>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
            return services;
        }
    }
}

namespace CoWorker.EntityFramework.Conventions
{
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Options;

    public class NeverNullConvention : IPropertyAddedConvention, IConfigureOptions<ConventionSet>
    {
        public InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder)
        {
            propertyBuilder.IsRequired(true, ConfigurationSource.Convention);
            return propertyBuilder;
        }

        public void Configure(ConventionSet options)
            => options.PropertyAddedConventions.Add(this);
    }
}

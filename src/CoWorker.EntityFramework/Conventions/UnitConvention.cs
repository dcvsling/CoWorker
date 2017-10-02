namespace CoWorker.EntityFramework.EntityParts.Conventions
{
    using CoWorker.EntityFramework.EntityParts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Options;
    public class UnitConvention<TUnit> : IPropertyAddedConvention, IConfigureOptions<ConventionSet>
    {
        public InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder)
        {
            var validtype = propertyBuilder.Metadata.ClrType;
            validtype = validtype.HasElementType ? validtype.GetElementType() : validtype;
            if (!typeof(TUnit).IsAssignableFrom(validtype)) return propertyBuilder;
            var name = propertyBuilder.Metadata.Name;
            var clrtype = propertyBuilder.Metadata.ClrType;
            propertyBuilder.Metadata.DeclaringEntityType
                .Builder
                .Owns(clrtype, name, ConfigurationSource.Convention)
                .DeleteBehavior(DeleteBehavior.Restrict,ConfigurationSource.Convention);
            return propertyBuilder;
        }

        public void Configure(ConventionSet options)
            => options.PropertyAddedConventions.Add(this);
    }
}

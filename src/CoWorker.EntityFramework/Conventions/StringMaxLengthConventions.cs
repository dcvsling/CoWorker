namespace CoWorker.EntityFramework.Conventions
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Options;
    public class StringMaxLengthConventions : IPropertyAddedConvention, IConfigureOptions<ConventionSet>
	{
		private EFConventionsOptions options;

		public String Name => nameof(StringMaxLengthConventions);

		public StringMaxLengthConventions(IOptions<EFConventionsOptions> options)
		{
			this.options = options.Value;

		}
		public InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder)
		{
			if (IsString(propertyBuilder) || !HasMaxLength(propertyBuilder)) return propertyBuilder;
			propertyBuilder.HasMaxLength(options.MaxLength[propertyBuilder.Metadata.Name], ConfigurationSource.Convention);
			return propertyBuilder;
		}

		private bool IsString(InternalPropertyBuilder propertyBuilder)
			=> propertyBuilder.Metadata.ClrType != typeof(string);
		private bool HasMaxLength(InternalPropertyBuilder propertyBuilder)
			=> options.MaxLength.ContainsKey(propertyBuilder.Metadata.Name);
		public void Configure(ConventionSet builder)
			=> builder.PropertyAddedConventions.Add(this);
	}
    
    public class ForceRemoveAllCascadeOnDeleteConvention : IForeignKeyAddedConvention, IConfigureOptions<ConventionSet>
    {
        public InternalRelationshipBuilder Apply(InternalRelationshipBuilder relationshipBuilder)
        {
            relationshipBuilder.Metadata.SetDeleteBehavior(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict, ConfigurationSource.Convention);
            return relationshipBuilder;
        }
        public void Configure(ConventionSet options)
            => options.ForeignKeyAddedConventions.Add(this);
    }
}

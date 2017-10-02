using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CoWorker.EntityFramework.EntityParts.Conventions
{
    using System;
    using CoWorker.EntityFramework.EntityParts;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Options;
    using CoWorker.EntityFramework.ValueGenerators;

    public class AutoLogDatetimeConventions : IPropertyAddedConvention, IConfigureOptions<ConventionSet>
	{
        private readonly ValueGeneratorFactory _factory;
        public AutoLogDatetimeConventions(ValueGeneratorFactory factory)
        {
            _factory = factory;
        }
		public String Name => nameof(AutoLogDatetimeConventions);
        
        public InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder)
		{
			ApplyCreateDate(propertyBuilder);
			ApplyModifyDate(propertyBuilder);
            //ApplyCreator(propertyBuilder);
            //ApplyModifier(propertyBuilder);
			return propertyBuilder;
		}

		public void Configure(ConventionSet builder)
			=> builder.PropertyAddedConventions.Add(this);

        private ValueGenerator Create(IProperty prop, IEntityType entry)
            => _factory.Create(prop);

		private void ApplyCreateDate(InternalPropertyBuilder propertyBuilder)
		{
			if (EntityFrameworkDefault.CreateDate != propertyBuilder.Metadata.Name) return;
			propertyBuilder.HasValueGenerator(typeof(DatetimeValueGenerator), ConfigurationSource.Convention);
			propertyBuilder.ValueGenerated(ValueGenerated.OnAddOrUpdate, ConfigurationSource.Convention);
        }

		private void ApplyModifyDate(InternalPropertyBuilder propertyBuilder)
		{
			if (EntityFrameworkDefault.ModifyDate != propertyBuilder.Metadata.Name) return;
			propertyBuilder.HasValueGenerator(typeof(DatetimeValueGenerator), ConfigurationSource.Convention);
			propertyBuilder.ValueGenerated(ValueGenerated.OnAddOrUpdate, ConfigurationSource.Convention);
		}

        private void ApplyModifier(InternalPropertyBuilder propertyBuilder)
        {
            if (EntityFrameworkDefault.Modifier != propertyBuilder.Metadata.Name) return;
            propertyBuilder.HasValueGenerator(Create, ConfigurationSource.Convention);
            propertyBuilder.ValueGenerated(ValueGenerated.OnAddOrUpdate, ConfigurationSource.Convention);
        }

        private void ApplyCreator(InternalPropertyBuilder propertyBuilder)
        {
            if (propertyBuilder.Metadata.Name != EntityFrameworkDefault.Creator) return;
            propertyBuilder.HasValueGenerator(Create, ConfigurationSource.Convention);
            propertyBuilder.ValueGenerated(ValueGenerated.OnAdd, ConfigurationSource.Convention);
        }
    }
    
}

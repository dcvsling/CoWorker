
namespace CoWorker.EntityFramework.EntityParts.Conventions
{
    using System;
    using CoWorker.EntityFramework.EntityParts;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Options;
    using CoWorker.EntityFramework.Conventions;

    public class AutoRowLogConvention : IEntityTypeAddedConvention, IConfigureOptions<ConventionSet>
	{
		private EFConventionsOptions options;
		public AutoRowLogConvention(IOptions<EFConventionsOptions> options)
			=> this.options = options.Value;

		public String Name => nameof(AutoRowLogConvention);

		public InternalEntityTypeBuilder Apply(InternalEntityTypeBuilder entityTypeBuilder)
		{
			ApplyGenerateColumn(entityTypeBuilder, EntityFrameworkDefault.CreateDate, typeof(DateTime?));
			ApplyGenerateColumn(entityTypeBuilder, EntityFrameworkDefault.ModifyDate, typeof(DateTime?));
			return entityTypeBuilder;
		}
        
		private void ApplyGenerateColumn(InternalEntityTypeBuilder entityTypeBuilder,string name,Type type)
		{
            entityTypeBuilder.Metadata.GetOrAddProperty(name, type);
		}

		public void Configure(ConventionSet builder)
			=> builder.EntityTypeAddedConventions.Add(this);
	}


   
}

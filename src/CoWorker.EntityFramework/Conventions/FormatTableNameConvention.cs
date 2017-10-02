
namespace CoWorker.EntityFramework.Conventions
{
    using System;
    using System.Linq;
    using System.Collections.Immutable;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.Extensions.Options;

    public class FormatTableNameConvention : IEntityTypeAddedConvention, IConfigureOptions<ConventionSet>
	{
		public String Name => nameof(FormatTableNameConvention);
		public EFConventionsOptions options;
		public FormatTableNameConvention(IOptions<EFConventionsOptions> options)
		{
			this.options = options.Value;
		}
		public InternalEntityTypeBuilder Apply(InternalEntityTypeBuilder entityTypeBuilder)
		{
			entityTypeBuilder.Metadata.SetAnnotation(
				RelationalAnnotationNames.TableName,
				options.RemoveName.Aggregate(
					entityTypeBuilder.Metadata.ClrType.ToShortString(),
					(seed, next) => seed.Remove(next)),
				ConfigurationSource.Convention);
			return entityTypeBuilder;
		}

		public void Configure(ConventionSet builder)
			=> builder.EntityTypeAddedConventions.Add(this);
	}
}

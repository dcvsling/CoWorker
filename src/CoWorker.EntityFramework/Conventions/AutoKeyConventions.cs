namespace CoWorker.EntityFramework.Conventions
{
    using Microsoft.Extensions.Options;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Collections.Immutable;
	using Microsoft.EntityFrameworkCore.Metadata.Conventions;
	using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
	using Microsoft.EntityFrameworkCore.Metadata.Internal;
    public class AutoKeyConventions : IEntityTypeAddedConvention, IConfigureOptions<ConventionSet>
	{
		public String Name => nameof(AutoKeyConventions);
		private string Id = nameof(Id);
		private IReadOnlyList<string> PK => new List<string>() { Id }.ToImmutableList();
		public InternalEntityTypeBuilder Apply(InternalEntityTypeBuilder entityTypeBuilder)
		{
			if (null != entityTypeBuilder.Metadata.FindPrimaryKey()
				|| null != entityTypeBuilder.Metadata.FindProperty(Id)
				|| (entityTypeBuilder.Metadata.ClrType?.GetProperties().Any(x => x.Name == Id) ?? false))
				return entityTypeBuilder;
			entityTypeBuilder.Property("Id",typeof(int), ConfigurationSource.Convention)
				.Attach(entityTypeBuilder,ConfigurationSource.Convention);
			entityTypeBuilder.PrimaryKey(PK, ConfigurationSource.Convention);
			return entityTypeBuilder;
		}

		public void Configure(ConventionSet options)
			=> options.EntityTypeAddedConventions.Insert(0, this);
	}
}

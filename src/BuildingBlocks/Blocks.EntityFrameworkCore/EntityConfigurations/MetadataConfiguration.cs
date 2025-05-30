﻿using Blocks.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFrameworkCore;

public abstract class MetadataConfiguration<T> : IEntityTypeConfiguration<T>
		where T : class, IMetadataEntity
{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
				builder.ToTable(typeof(T).Name);

				builder.SeedFromFile();
		}
}
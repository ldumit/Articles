﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blocks.Entitities;

namespace Blocks.EntityFrameworkCore;

public abstract class ValueObjectConfiguration<T> : IEntityTypeConfiguration<T> 
    where T : class
{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
				//add base implementation here
		}

		protected virtual string EntityName => typeof(T).Name;
}

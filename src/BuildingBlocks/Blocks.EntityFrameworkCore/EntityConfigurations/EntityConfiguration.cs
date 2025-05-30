﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blocks.Entitities;

namespace Blocks.EntityFrameworkCore;

public abstract class EntityConfiguration<T> : EntityConfiguration<T, int> 
    where T : class, IEntity
{
    protected virtual bool HasGeneratedId => true;
		public override void Configure(EntityTypeBuilder<T> builder)
    {
				if(HasGeneratedId)
				    builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnOrder(0);
        else
						builder.Property(e => e.Id).ValueGeneratedNever().HasColumnOrder(0);
				base.Configure(builder);
		}
}

public abstract class EntityConfiguration<T, TKey> : IEntityTypeConfiguration<T> 
    where T : class, IEntity<TKey>
    where TKey: struct
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        //builder.ToTable(new Pluralizer().Pluralize(typeof(T).Name));
        //builder.ToTable(this.EntityName);

        builder.HasKey(e => e.Id);
        builder.SeedFromFile();
    }

    protected virtual string EntityName => typeof(T).Name;
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Articles.Entitities;
using Articles.System;

namespace Articles.EntityFrameworkCore;

public abstract class EntityConfiguration<T> : EntityConfiguration<T, int> 
    where T : class, IEntity<int>
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(col => col.Id).ValueGeneratedOnAdd();
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

        builder.HasKey(col => col.Id);
        builder.SeedFromFile();
    }

    protected virtual string EntityName => typeof(T).Name;
}

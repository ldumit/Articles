using Blocks.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFrameworkCore;

public abstract class AuditedEntityConfiguration<T> : AuditedEntityConfiguration<T, int>
	  where T : class, IEntity<int>, IAggregateEntity<int>
{
}

public abstract class AuditedEntityConfiguration<T, TKey> : EntityConfiguration<T, TKey> 
    where T : class, IEntity<TKey>, IAggregateEntity<TKey>
    where TKey : struct
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.CreatedOn).IsRequired().HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.CreatedById).IsRequired();
        builder.Property(e => e.LasModifiedOn);
        builder.Property(e => e.LastModifiedById);
    }
}
using Articles.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.EntityFrameworkCore;

public abstract class AuditedEntityConfiguration<T> : AuditedEntityConfiguration<T, int>
	  where T : class, IEntity<int>, IAuditedEntity<int>
{
}

public abstract class AuditedEntityConfiguration<T, TKey> : EntityConfiguration<T, TKey> 
    where T : class, IEntity<TKey>, IAuditedEntity<TKey>
    where TKey : struct
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.CreatedOn).IsRequired().HasDefaultValue(DateTime.UtcNow);
        builder.Property(e => e.CreatedById).IsRequired();
        builder.Property(e => e.LasModifiedOn);
        builder.Property(e => e.LastModifiedById);
    }
}
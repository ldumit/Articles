using Articles.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.EntityFrameworkCore;

public abstract class AuditedEntityConfigurationBase<T> : AuditedEntityConfigurationBase<T, int>
	  where T : class, IEntity<int>, IAuditedEntity<int>
{
}

public abstract class AuditedEntityConfigurationBase<T, TKey> : EntityConfiguration<T, TKey> 
    where T : class, IEntity<TKey>, IAuditedEntity<TKey>
    where TKey : struct
{
    public override void Configure(EntityTypeBuilder<T> entity)
    {
        base.Configure(entity);

        entity.Property(u => u.CreatedOn).IsRequired(true).HasDefaultValueSql("getdate()");
        entity.Property(u => u.CreatedById).IsRequired();
        entity.Property(u => u.LasModifiedOn).HasDefaultValue(DateTime.UtcNow);
        entity.Property(u => u.LastModifiedById).IsRequired();
    }
}
using Articles.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.EntityFrameworkCore;

public abstract class AuditedEntityConfigurationBase<T> : AuditedEntityConfigurationBase<T, int> where T : AuditedEntity<int>
{
}
public abstract class AuditedEntityConfigurationBase<T, TKey> : EntityConfigurationBase<T, TKey> where T : AuditedEntity<TKey>
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
using Blocks.Entitities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFrameworkCore;

public abstract class TenantEntityConfigurationBase<T> : EntityConfiguration<T> where T : TenantEntity
{
    public override void Configure(EntityTypeBuilder<T> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.TenantId);
        entity.HasKey(e => new { e.TenantId, e.Id });
    }
}
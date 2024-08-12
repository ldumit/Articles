using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class AssetTypeEntityConnfiguration : EnumEntityConfiguration<AssetType, Domain.Enums.AssetType>
{
    public override void Configure(EntityTypeBuilder<AssetType> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.DefaultCategoryId);
    }
}

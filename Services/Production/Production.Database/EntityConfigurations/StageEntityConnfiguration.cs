using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class StageEntityConnfiguration : EnumEntityConfigurationBase<Stage, Domain.Enums.ArticleStage>
{
    public override void Configure(EntityTypeBuilder<Stage> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.Description).HasMaxLength(Constraints.FiveHundred).IsRequired();
    }
}

using Articles.Abstractions;
using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class StageEntityConnfiguration : EnumEntityConfiguration<Stage, ArticleStage>
{
    public override void Configure(EntityTypeBuilder<Stage> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.Description).HasMaxLength(Constraints.C512).IsRequired();
    }
}

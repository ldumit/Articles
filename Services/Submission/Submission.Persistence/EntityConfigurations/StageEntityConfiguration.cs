using Articles.Abstractions;
using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class StageEntityConfiguration : EnumEntityConfiguration<Stage, ArticleStage>
{
    public override void Configure(EntityTypeBuilder<Stage> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.Info).HasMaxLength(Constraints.C512).IsRequired();
    }
}

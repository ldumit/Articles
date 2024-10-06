using Articles.EntityFrameworkCore;
using ArticleTimeline.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleTimeline.Persistence.EntityConfigurations;

public class TimelineTemplateEntityConfiguration : EntityConfiguration<TimelineTemplate>
{
    public override void Configure(EntityTypeBuilder<TimelineTemplate> entity)
    {
        entity.Property(e => e.SourceType).HasEnumConversion();
				//entity.Property(e => e.ArticleStage).HasEnumConversion();
				entity.Property(e => e.TitleTemplate).HasMaxLength(Constraints.C256);
    }
}

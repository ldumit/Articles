using Articles.EntityFrameworkCore;
using Articles.EntityFrameworkCore.EntityConfigurations;
using ArticleTimeline.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleTimeline.Persistence.EntityConfigurations;

public class TimelineTemplateEntityConfiguration : MetadataConfiguration<TimelineTemplate>
{
    public override void Configure(EntityTypeBuilder<TimelineTemplate> builder)
    {
				base.Configure(builder);

				builder.HasKey(e => new { e.SourceType, e.SourceId});

				builder.Property(e => e.SourceType).HasEnumConversion().IsRequired();
				//entity.Property(e => e.ArticleStage).HasEnumConversion()IsRequired();
				builder.Property(e => e.SourceId).HasMaxLength(Constraints.C64).IsRequired();
				builder.Property(e => e.TitleTemplate).HasMaxLength(Constraints.C256).IsRequired();
    }
}

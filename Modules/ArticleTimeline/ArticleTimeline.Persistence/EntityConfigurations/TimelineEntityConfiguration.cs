using Articles.EntityFrameworkCore;
using ArticleTimeline.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleTimeline.Persistence.EntityConfigurations;

public class TimelineEntityConfiguration : EntityConfiguration<Timeline>
{
    public override void Configure(EntityTypeBuilder<Timeline> entity)
    {
				base.Configure(entity);

				entity.Property(e => e.PreviousStage).HasEnumConversion().IsRequired();
        entity.Property(e => e.SourceType).HasEnumConversion().IsRequired();
        entity.Property(e => e.Title).HasMaxLength(Constraints.C256).IsRequired();
        entity.Property(e => e.Description).IsRequired();
        entity.Property(e => e.FileId);
        //entity.Property(e => e.RoleType).HasEnumConversion();


				entity.HasOne(e => e.Template).WithMany()
						.HasForeignKey(e => new { e.SourceType, e.SourceId})
						.IsRequired();

		}
}

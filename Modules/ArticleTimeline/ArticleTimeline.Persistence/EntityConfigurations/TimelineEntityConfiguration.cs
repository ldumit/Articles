using Articles.EntityFrameworkCore;
using ArticleTimeline.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleTimeline.Persistence.EntityConfigurations;

public class TimelineEntityConfiguration : EntityConfiguration<Timeline>
{
    public override void Configure(EntityTypeBuilder<Timeline> entity)
    {
        entity.Property(e => e.Stage).HasEnumConversion();
        entity.Property(e => e.SourceType).HasEnumConversion();
        entity.Property(e => e.Title).HasMaxLength(Constraints.C256);
        entity.Property(e => e.Description);
        entity.Property(e => e.FileId);
        entity.Property(e => e.RoleType).HasEnumConversion();


				entity.HasOne(e => e.Template).WithMany()
						.HasForeignKey(e => e.TemplateId)
						.IsRequired();

		}
}

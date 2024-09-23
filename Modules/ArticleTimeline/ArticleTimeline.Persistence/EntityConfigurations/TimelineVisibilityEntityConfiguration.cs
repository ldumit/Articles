using Articles.EntityFrameworkCore;
using ArticleTimeline.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleTimeline.Persistence.EntityConfigurations;

public class TimelineVisibilityEntityConfiguration : EntityConfiguration<TimelineVisibility>
{
    public override void Configure(EntityTypeBuilder<TimelineVisibility> entity)
    {
        entity.Property(e => e.SourceType);
        entity.Property(e => e.SourceId);
        entity.Property(e => e.RoleType);
        entity.Property(e => e.Stage);
    }
}

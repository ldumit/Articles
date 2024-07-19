using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

internal class ArticleCurrentStageEntityConfiguration : IEntityTypeConfiguration<ArticleCurrentStage>
{
    public void Configure(EntityTypeBuilder<ArticleCurrentStage> entity)
    {
        entity.HasKey(e => e.ArticleId);
        entity.HasIndex(e => e.StageId).IsUnique();

        entity.Property(e => e.StageId).IsRequired().HasConversion<int>();

        entity.HasOne(e => e.Stage).WithOne()
            .HasForeignKey<ArticleCurrentStage>(e => e.StageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

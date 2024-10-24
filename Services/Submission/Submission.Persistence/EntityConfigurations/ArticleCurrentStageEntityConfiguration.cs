using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class ArticleCurrentStageEntityConfiguration : IEntityTypeConfiguration<ArticleCurrentStage>
{
    public void Configure(EntityTypeBuilder<ArticleCurrentStage> entity)
    {
				entity.HasKey(e => e.ArticleId);
				entity.HasIndex(e => e.Stage);

        entity.Property(e => e.Stage).IsRequired().HasEnumConversion();


				entity.HasOne<Stage>().WithMany()
					 .HasForeignKey(e => e.Stage)
					 .HasPrincipalKey(e => e.Name)
					 .IsRequired()
					 .OnDelete(DeleteBehavior.Restrict);
		}
}

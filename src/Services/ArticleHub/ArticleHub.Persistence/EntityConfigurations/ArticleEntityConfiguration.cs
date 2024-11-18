using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Blocks.EntityFrameworkCore;
using ArticleHub.Domain;

namespace ArticleHub.Persistence.EntityConfigurations;
public class ArticleEntityConfiguration : EntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.Title);

        //talk - using constants instead of direct numbers
        entity.Property(e => e.Title).HasMaxLength(Constraints.C256).IsRequired();
        entity.Property(e => e.Doi).HasMaxLength(Constraints.C64).IsRequired();
        entity.Property(e => e.Stage).HasEnumConversion().IsRequired();

        entity.Property(e => e.SubmitedOn).IsRequired();
        entity.Property(e => e.AcceptedOn).IsRequired();

				entity.HasOne(e => e.SubmitedBy).WithMany()
            .HasForeignKey(e => e.SubmitedById)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Journal).WithMany(e => e.Articles)
            .HasForeignKey(e => e.JournalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

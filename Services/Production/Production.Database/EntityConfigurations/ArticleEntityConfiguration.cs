using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;
using Common.Persistence.EntityConfigurations;

namespace Production.Database.EntityConfigurations;
public class ArticleEntityConfiguration : TenantEntityConfigurationBase<Article>
{
    protected override void ConfigureMore(EntityTypeBuilder<Article> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.Id }).HasName("article_pkey");

        entity.HasIndex(e => e.Title, "article_index");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.Id);
        entity.Property(e => e.AcceptedOn).HasColumnType("timestamp without time zone");

        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.JournalId)
            .HasDefaultValueSql("0")
            ;
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.PublishedOn)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.StageId);
        entity.Property(e => e.Title);

        entity.HasOne(d => d.Journal).WithMany(p => p.Articles)
            .HasForeignKey(d => new { d.TenantId, d.JournalId })
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("article_spaceId_journalId_fkey");
        entity.Property(e => e.FieldId);
        entity.Property(e => e.FieldName);

        entity.Property(e => e.RecommendedPublicationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.VolumeId);
        entity.Property(e => e.JournalTransferCompletedDate)
            .HasColumnType("timestamp without time zone")
            ;
    }
}

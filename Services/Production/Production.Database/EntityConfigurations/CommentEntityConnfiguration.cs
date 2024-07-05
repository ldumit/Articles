using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class CommentEntityConnfiguration : TenantEntityConfigurationBase<Comment>
{
    protected override void ConfigureMore(EntityTypeBuilder<Comment> entity)
    {
        entity.HasKey(e => e.Id).HasName("comment_pkey");

        entity.ToTable("comment");

        entity.HasIndex(e => new { e.TenantId, e.ArticleId, e.TypeId }, "comment_articleId_spaceId_typeId_key").IsUnique();

        entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"comment_id_seq\"'::regclass)");
        entity.Property(e => e.ArticleId);
        entity.Property(e => e.CommentModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.ModifiedBy);
        entity.Property(e => e.TenantId);
        entity.Property(e => e.Text);
        entity.Property(e => e.TypeId);

        //entity.HasOne(d => d.Type).WithMany(p => p.Comments)
        //    .HasForeignKey(d => d.TypeId)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .HasConstraintName("comment_typeId_fkey");

        entity.HasOne(d => d.Article).WithMany(p => p.Comments)
            .HasForeignKey(d => new { d.TenantId, d.ArticleId })
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("comment_articleId_fkey");
    }
}

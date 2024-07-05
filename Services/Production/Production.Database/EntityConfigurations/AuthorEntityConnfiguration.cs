using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class AuthorEntityConnfiguration : TenantEntityConfigurationBase<Author>
{
    protected override void ConfigureMore(EntityTypeBuilder<Author> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.Id }).HasName("author_pkey");

        entity.ToTable("author");

        entity.HasIndex(e => e.ArticleId, "author_articleId_idx");

        entity.HasIndex(e => new { e.FirstName, e.LastName }, "author_index");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.Id)
            .HasDefaultValueSql("nextval('\"ArticleAuthor_ArticleAuthorId_seq\"'::regclass)")
            ;
        entity.Property(e => e.ArticleId);
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.FirstName);
        entity.Property(e => e.FullName);
        entity.Property(e => e.LastName);
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.RoleCode)
            .HasDefaultValueSql("'default'::text")
            ;
        entity.Property(e => e.RoleId);
        entity.Property(e => e.UserId);
        entity.Property(e => e.Email);
        entity.Property(e => e.Country);

        entity.HasOne(d => d.Article).WithMany(p => p.Authors)
            .HasForeignKey(d => new { d.TenantId, d.ArticleId })
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("author_articleId_spaceId_fkey");
    }
}

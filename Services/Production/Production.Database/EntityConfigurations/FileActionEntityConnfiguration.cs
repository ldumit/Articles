using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class FileActionEntityConnfiguration : TenantEntityConfigurationBase<FileAction>
{
    protected override void ConfigureMore(EntityTypeBuilder<FileAction> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.Id }).HasName("fileAction_pkey");

        entity.ToTable("fileAction");

        entity.HasIndex(e => new { e.TenantId, e.FileId }, "fileAction_spaceId_fileId_idx");

        entity.HasIndex(e => e.TypeId, "fileAction_typeId_idx");

        entity.HasIndex(e => e.UserId, "fileAction_userId_idx");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.Id)
            .HasDefaultValueSql("nextval('\"ArticleFileAction_ArticleFileActionId_seq\"'::regclass)")
            ;
        entity.Property(e => e.Comment);
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.FileId);
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.Timestamp)
            .HasDefaultValueSql("now()")
            ;
        entity.Property(e => e.TypeId);
        entity.Property(e => e.UserId);

        //entity.HasOne(d => d.User).WithMany(p => p.FileActions)
        //    .HasForeignKey(d => d.UserId)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .HasConstraintName("fileAction_userId_fkey");

        entity.HasOne(d => d.File).WithMany(p => p.FileActions)
            .HasForeignKey(d => new { d.TenantId, d.FileId })
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("ProductionArticleFileAction_ProductionArticleFileId_SpaceId_");
    }
}

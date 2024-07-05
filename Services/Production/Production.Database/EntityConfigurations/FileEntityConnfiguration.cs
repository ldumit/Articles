using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace ProductionForum.Data.EFCORE.EntityConfigurations;

public class FileEntityConnfiguration : TenantEntityConfigurationBase<Production.Domain.Entities.File>
{
    protected override void ConfigureMore(EntityTypeBuilder<Production.Domain.Entities.File> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.Id }).HasName("file_pkey");

        entity.ToTable("file");

        entity.HasIndex(e => e.AssetId, "file_spaceId_assetId_idx");

        entity.HasIndex(e => e.StatusId, "file_statusId_idx");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.Id)
            .HasDefaultValueSql("nextval('\"ArticleFile_Id_seq\"'::regclass)")
            ;
        entity.Property(e => e.AssetId);
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.ErrorMessage);
        entity.Property(e => e.Extension);
        entity.Property(e => e.FileServerId);
        entity.Property(e => e.IsLatest)
            .IsRequired()
            .HasDefaultValueSql("true")
            ;
        entity.Property(e => e.LastActionDate)
            .HasDefaultValueSql("now()")
            ;
        entity.Property(e => e.LastActionUserId);
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.Name)
            .HasComment("Final name of the file after renaming")
            ;
        entity.Property(e => e.OriginalName)
            .HasComment("Full file name, with extension")
            ;
        entity.Property(e => e.Size)
            .HasComment("Size of the file in kilobytes")
            ;
        entity.Property(e => e.StatusId);
        entity.Property(e => e.Version);

        //entity.HasOne(d => d.Status).WithMany(p => p.Files)
        //    .HasForeignKey(d => d.StatusId)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .HasConstraintName("file_statusId_fkey");

        entity.HasOne(d => d.Asset).WithMany(p => p.Files)
            .HasForeignKey(d => new { d.TenantId, d.AssetId })
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("file_spaceId_assetId_fkey");

        entity.Property(e => e.UploadBatchId).IsRequired(false);
    }
}

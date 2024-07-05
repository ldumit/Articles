using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

internal class AssetEntityConnfiguration : TenantEntityConfigurationBase<Asset>
{
    protected override void ConfigureMore(EntityTypeBuilder<Asset> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.Id }).HasName("asset_pkey");

        entity.HasIndex(e => new { e.Id, e.ArticleId, e.TenantId, e.ProductionArticleAssetNumber }, "ProductionArticleAsset_ProductionArticleAssetId_ProductionArtic").IsUnique();

        entity.HasIndex(e => new { e.TenantId, e.ArticleId }, "asset_spaceId_articleId_idx");

        entity.HasIndex(e => e.StatusId, "asset_statusId_idx");

        entity.HasIndex(e => e.TypeId, "asset_typeId_idx");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Asset_Id_seq\"'::regclass)");
        entity.Property(e => e.ArticleId);
        entity.Property(e => e.AssetNumber).HasDefaultValueSql("0");
        entity.Property(e => e.CategoryId).HasConversion<int>();
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.ModifiedBy);
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("now()")
            ;
        entity.Property(e => e.Name);
        entity.Property(e => e.StatusId);
        entity.Property(e => e.TypeId);

        //entity.HasOne(d => d.Type).WithMany(p => p.Assets)
        //    .HasForeignKey(d => d.TypeId)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .HasConstraintName("asset_typeId_fkey");

        entity.HasOne(d => d.Article).WithMany(p => p.Assets)
            .HasForeignKey(d => new { d.TenantId, d.ArticleId })
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("asset_spaceId_articleId_fkey");
        entity.Property(e => e.LatestFileId);
        entity.HasOne(d => d.LatestFile).WithOne()
            .HasForeignKey<Asset>(d => new { d.TenantId, d.LatestFileId }).IsRequired(false);
    }
}

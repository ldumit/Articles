using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

internal class AssetEntityConnfiguration : AuditedEntityConfigurationBase<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> entity)
    {
        base.Configure(entity);

        //talk - simple vs composite indexes
        // we don't need the following indexes because we are always taking the assets based on the articleId
        //entity.HasIndex(e => e.StatusId);
        //entity.HasIndex(e => e.TypeId);

        entity.Property(e => e.Name).HasMaxLength(Constraints.Fifty).IsRequired();
        entity.Property(e => e.AssetNumber).HasDefaultValue(Constraints.Zero);
        entity.Property(e => e.StatusId).HasConversion<int>().IsRequired();
        entity.Property(e => e.CategoryId).HasConversion<int>().IsRequired();
        entity.Property(e => e.TypeId).HasConversion<int>().IsRequired();

        //entity.Property(e => e.ArticleId);
        entity.HasOne(d => d.Article).WithMany(p => p.Assets)
            .HasForeignKey(d => d.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Type).WithMany()
            .HasForeignKey(o => o.TypeId)
            .IsRequired();

        entity.HasMany(e => e.Files).WithOne(e => e.Asset)
            .HasForeignKey(e => e.AssetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.LatestFile).WithOne(e => e.Asset)
            .HasForeignKey<AssetLatestFile>(e => e.AssetId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

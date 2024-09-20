using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.Persistence.EntityConfigurations;

internal class AssetEntityConfiguration : AuditedEntityConfiguration<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> entity)
    {
        base.Configure(entity);

        //talk - simple vs composite indexes
        // we don't need the following indexes because we are always taking the assets based on the articleId
        //entity.HasIndex(e => e.StatusId);
        //entity.HasIndex(e => e.TypeId);

        entity.Property(e => e.Name).HasMaxLength(Constraints.C64).IsRequired();
        entity.Property(e => e.AssetNumber).HasDefaultValue(0);
        entity.Property(e => e.State).HasEnumConversion().IsRequired();
        entity.Property(e => e.CategoryId).HasConversion<int>().HasDefaultValue(AssetCategory.Core).IsRequired();
        entity.Property(e => e.TypeCode).HasEnumConversion().IsRequired();

        entity.HasOne(d => d.Article).WithMany(p => p.Assets)
            .HasForeignKey(d => d.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Type).WithMany()
            .HasForeignKey(e => e.TypeCode)
            .HasPrincipalKey(e => e.Code)
            .IsRequired();

        entity.HasMany(e => e.Files).WithOne(e => e.Asset)
            .HasForeignKey(e => e.AssetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.CurrentFileLink).WithOne(e => e.Asset)
            .HasForeignKey<AssetCurrentFileLink>(e => e.AssetId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

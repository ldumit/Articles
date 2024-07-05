using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class AssetTypeEntityConnfiguration : EnumEntityConfigurationBase<AssetType, Domain.Enums.AssetType>
{
    protected override void ConfigureMore(EntityTypeBuilder<AssetType> entity)
    {
        entity.HasKey(e => e.Id).HasName("assetType_pkey");

        entity.ToTable("assetType");

        entity.HasIndex(e => e.CategoryId, "assetType_categoryId_idx");

        entity.HasIndex(e => e.Code, "assetType_code_key").IsUnique();

        entity.HasIndex(e => e.Name, "assetType_name_key").IsUnique();

        entity.Property(e => e.Id)
            .ValueGeneratedNever()
            ;
        entity.Property(e => e.CategoryId);
        entity.Property(e => e.Code);
        entity.Property(e => e.DefaultCategoryId);
        entity.Property(e => e.Name);

        try
        {
            var assetTypesData = JsonConvert.DeserializeObject<List<AssetType>>(System.IO.File.ReadAllText(@"../ProductionForum.Data.EFCORE/EntityConfigurations/MasterData/AssetTypes.json"));
            if (assetTypesData != null)
            {
                entity.HasData(assetTypesData);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("EX:---->" + ex.ToString());
        }
    }
}

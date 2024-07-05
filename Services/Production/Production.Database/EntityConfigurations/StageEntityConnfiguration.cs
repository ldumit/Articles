using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.Database.EntityConfigurations;

public class StageEntityConnfiguration : EnumEntityConfigurationBase<Stage, ArticleStagesCode>
{
    protected override void ConfigureMore(EntityTypeBuilder<Stage> entity)
    {
        entity.HasKey(e => e.Id).HasName("stage_pkey");

        entity.ToTable("stage");

        entity.HasIndex(e => e.Code, "stage_code_key").IsUnique();

        entity.HasIndex(e => e.Name, "stage_name_key").IsUnique();

        entity.Property(e => e.Id)
            .HasDefaultValueSql("nextval('\"ArticleStage_Code_seq\"'::regclass)");
        entity.Property(e => e.Code);
        entity.Property(e => e.Name);
        entity.Property(e => e.Description);

        try
        {
            var stagesData = JsonConvert.DeserializeObject<List<Stage>>(System.IO.File.ReadAllText(@"../ProductionForum.Data.EFCORE/EntityConfigurations/MasterData/Stages.json"));
            if (stagesData != null)
            {
                entity.HasData(stagesData);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("EX:---->" + ex.ToString());
        }
    }
}

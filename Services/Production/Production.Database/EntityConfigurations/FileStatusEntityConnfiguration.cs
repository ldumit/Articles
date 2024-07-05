using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.Database.EntityConfigurations;

public class FileStatusEntityConnfiguration : EntityConfigurationBase<Domain.Entities.FileStatus, Domain.Enums.FileStatus>
{
    protected override void ConfigureMore(EntityTypeBuilder<Domain.Entities.FileStatus> entity)
    {
        entity.HasKey(e => e.Id).HasName("fileStatus_pkey");

        entity.ToTable("fileStatus");

        entity.HasIndex(e => e.Code, "fileStatus_code_key").IsUnique();

        entity.HasIndex(e => e.Name, "fileStatus_name_key").IsUnique();

        entity.Property(e => e.Id)
            .ValueGeneratedNever()
            ;
        entity.Property(e => e.Code);

        entity.Property(e => e.Name);

        try
        {
            var fileStatusesData = JsonConvert.DeserializeObject<List<Domain.Entities.FileStatus>>(System.IO.File.ReadAllText(@"../ProductionForum.Data.EFCORE/EntityConfigurations/MasterData/FileStatuses.json"));
            if (fileStatusesData != null)
            {
                entity.HasData(fileStatusesData);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("EX:---->" + ex.ToString());
        }

    }
}

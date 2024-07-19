using Articles.Entitities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Articles.EntityFrameworkCore;

public abstract class EnumEntityConfigurationBase<T, TEnum> : EntityConfigurationBase<T, TEnum>
    where T : EnumEntity<TEnum>
    where TEnum : Enum
{
    public override void Configure(EntityTypeBuilder<T> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(e => e.Code).HasMaxLength(Constraints.Fifty).IsRequired();
        entity.Property(e => e.Name).HasMaxLength(Constraints.Fifty).IsRequired();

        Seed(entity);
    }

    protected void Seed(EntityTypeBuilder<T> entity)
    {
        try
        {
            var stagesData = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText($@"../Production.Database/MasterData/{base.EntityName}.json"));
            //var stagesData = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText($@"/MasterData/{base.EntityName}.json"));
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
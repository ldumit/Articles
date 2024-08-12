using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Articles.Entitities;

namespace Articles.EntityFrameworkCore;

public abstract class EntityConfiguration<T> : EntityConfiguration<T, int> 
    where T : class, IEntity<int>
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(col => col.Id).ValueGeneratedOnAdd();
    }
}

public abstract class EntityConfiguration<T, TKey> : IEntityTypeConfiguration<T> 
    where T : class, IEntity<TKey>
    where TKey: struct
{
    public virtual void Configure(EntityTypeBuilder<T> entity)
    {
        //builder.ToTable(new Pluralizer().Pluralize(typeof(T).Name));
        //builder.ToTable(this.EntityName);

        entity.Property(col => col.Id).ValueGeneratedOnAdd();
        entity.HasKey(col => col.Id);
    }

    protected virtual string EntityName => typeof(T).Name;

		//protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

		protected virtual void SeedFromFile(EntityTypeBuilder<T> entity)
		{
				try
				{
						var filePath = $"{AppContext.BaseDirectory}MasterData/{typeof(T).Name}.json";
            ///var filePath2 = $@"../Production.Persistence/MasterData/{EntityName}.json";

						var stagesData = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
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

		public virtual void SeedFromFile(string path, EntityTypeBuilder<T> entity)
    {
        try
        {

            var data = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
            if (data != null)
            {
                entity.HasData(data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("EX:---->" + ex.ToString());
        }
    }
}

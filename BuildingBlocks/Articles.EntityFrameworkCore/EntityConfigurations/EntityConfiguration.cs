using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Articles.Entitities;
using Articles.System;

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
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        //builder.ToTable(new Pluralizer().Pluralize(typeof(T).Name));
        //builder.ToTable(this.EntityName);

        builder.Property(col => col.Id).ValueGeneratedOnAdd();
        builder.HasKey(col => col.Id);

        //ConfigureEntity(builder);
    }

    protected virtual string EntityName => typeof(T).Name;

    //protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

    public virtual void Seed(string path, EntityTypeBuilder<T> entity)
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Articles.Entitities;
using Articles.System;

namespace Common.Persistence.EntityConfigurations
{
    public abstract class EntityConfigurationBase<T> : EntityConfigurationBase<T, int> where T : Entity<int>
    {
    }

    public abstract class EnumEntityConfigurationBase<T, TEnum> : EntityConfigurationBase<T, TEnum> where T : Entity<TEnum> //where TEnum : enum
    {
    }

    public abstract class TenantEntityConfigurationBase<T> : EntityConfigurationBase<T> where T : TenantEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
           // builder.HasKey(e => new { e.SpaceId, e.Id }).HasName($"{base.EntityName}_pkey");
        }
    }

    public abstract class EntityConfigurationBase<T, TKey> : IEntityTypeConfiguration<T> where T : Entity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //builder.ToTable(new Pluralizer().Pluralize(typeof(T).Name));
            builder.ToTable(this.EntityName);

            //builder.HasKey(col => col.Id);

            ConfigureMore(builder);
        }

        protected virtual string EntityName => typeof(T).Name.ToLowerFirstChar();

        protected abstract void ConfigureMore(EntityTypeBuilder<T> builder);

        public virtual void FillMasterTables(string path, EntityTypeBuilder<T> entity)
        {
            try
            {

                var data = JsonConvert.DeserializeObject<List<T>>(System.IO.File.ReadAllText(path));
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
    public abstract class EnumerationEntityBase<T> : IEntityTypeConfiguration<T> where T : EnumEntityCode
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //builder.ToTable(new Pluralizer().Pluralize(typeof(T).Name));
            builder.ToTable(typeof(T).Name.ToLowerFirstChar());

            //builder.HasKey(col => col.Id);

            ConfigureMore(builder);
        }

        protected abstract void ConfigureMore(EntityTypeBuilder<T> builder);

        public virtual void FillMasterTables(string path, EntityTypeBuilder<T> entity)
        {
            try
            {

                var data = JsonConvert.DeserializeObject<List<T>>(System.IO.File.ReadAllText(path));
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
    public abstract class DefaultEntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : DefaultEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //builder.ToTable(new Pluralizer().Pluralize(typeof(T).Name));
            builder.ToTable(typeof(T).Name.ToLowerFirstChar());

            //builder.HasKey(col => col.Id);

            ConfigureMore(builder);
        }

        protected abstract void ConfigureMore(EntityTypeBuilder<T> builder);
        public virtual void FillMasterTables(string path, EntityTypeBuilder<T> entity)
        {
            try
            {

                var data = JsonConvert.DeserializeObject<List<T>>(System.IO.File.ReadAllText(path));
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
}

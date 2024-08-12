using Articles.Entitities;
using Articles.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.EntityFrameworkCore;

public abstract class EnumEntityConfiguration<T, TEnum> : EntityConfiguration<T, TEnum>
    where T : EnumEntity<TEnum>, new()
    where TEnum : struct, Enum
{
    public override void Configure(EntityTypeBuilder<T> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(e => e.Code).HasEnumConversion().HasMaxLength(Constraints.C64).IsRequired();
        entity.Property(e => e.Name).HasMaxLength(Constraints.C64).IsRequired();

        SeedFromFile(entity);
    }

		protected void SeedFromEnum(EntityTypeBuilder<T> entity)
		{
				var entityValues = EnumExtensions.GetValues<TEnum>()
						.Select(v => new T() { Id = v, Code = v, Name = v.ToDescription() });						
				try
				{
						entity.HasData(entityValues);

				}
				catch (Exception ex)
				{
						Console.WriteLine("EX:---->" + ex.ToString());
				}
		}
}
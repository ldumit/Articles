using Articles.Entitities;
using Articles.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.EntityFrameworkCore;

public abstract class EnumEntityConfiguration<T, TEnum> : EntityConfiguration<T, TEnum>
    where T : EnumEntity<TEnum>, new()
    where TEnum : struct, Enum
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.Code).IsUnique();

        builder.Property(e => e.Code).HasEnumConversion().HasMaxLength(Constraints.C64).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(Constraints.C64).IsRequired();

				//builder.SeedFromFile();
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
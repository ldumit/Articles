using Blocks.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFrameworkCore;

public abstract class EnumEntityConfiguration<T, TEnum> : EntityConfiguration<T, TEnum>
    where T : EnumEntity<TEnum>
    where TEnum : struct, Enum
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.Name).HasEnumConversion().HasMaxLength(Constraints.C64).IsRequired().HasColumnOrder(1);
        builder.Property(e => e.Description).HasMaxLength(Constraints.C64).IsRequired().HasColumnOrder(2);
    }
}
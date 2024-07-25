using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.EntityFrameworkCore
{
    public static class BuilderExtensions
    {
        public static PropertyBuilder<TEnum> HasEnumConversion<TEnum>(this PropertyBuilder<TEnum> builder)
            where TEnum : Enum
        {
            return builder.HasConversion(
                e => e.ToString(),
                value => (TEnum)Enum.Parse(typeof(TEnum), value));
        }
    }
}

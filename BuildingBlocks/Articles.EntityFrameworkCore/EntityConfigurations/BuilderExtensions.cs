using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Immutable;
using System.Text.Json;

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

				public static PropertyBuilder<IReadOnlyList<string>> HasCsvListConversion(this PropertyBuilder<IReadOnlyList<string>> builder)
				{
						Func<IReadOnlyList<string>, string> serializeFunc = v => v != null && v.Any() ? string.Join(',', v) : string.Empty;
						Func<string, IReadOnlyList<string>> deserializeFunc = v =>
								!string.IsNullOrEmpty(v) ? v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>();

						var csvListConverter = new ValueConverter<IReadOnlyList<string>, string>(
								v => serializeFunc(v),
								v => deserializeFunc(v)
						);

						return builder.HasConversion(csvListConverter);
				}

				//public static PropertyBuilder<IReadOnlyList<string>> HasJsonListConversion(this PropertyBuilder<IReadOnlyList<string>> builder)
				//{
				//		Func<List<string>, string> serializeFunc = v => JsonSerializer.Serialize(v);
				//		Func<string, List<string>> deserializeFunc = v => JsonSerializer.Deserialize<List<string>>(v ?? "[]");

				//		var jsonListConverter = new ValueConverter<List<string>, string>(
				//				v => serializeFunc(v),
				//				v => deserializeFunc(v)
				//		);

				//		return builder.HasConversion(jsonListConverter);
				//}

				public static PropertyBuilder<IReadOnlyList<T>> HasJsonListConversion<T>(this PropertyBuilder<IReadOnlyList<T>> builder)
				{
						Func<IReadOnlyList<T>, string> serializeFunc = v => JsonSerializer.Serialize(v);
						Func<string, IReadOnlyList<T>> deserializeFunc = v => JsonSerializer.Deserialize<IReadOnlyList<T>>(v ?? "[]");

						var jsonListConverter = new ValueConverter<IReadOnlyList<T>, string>(
								v => serializeFunc(v),
								v => deserializeFunc(v)
						);

						return builder.HasConversion(jsonListConverter);
				}
		}
}

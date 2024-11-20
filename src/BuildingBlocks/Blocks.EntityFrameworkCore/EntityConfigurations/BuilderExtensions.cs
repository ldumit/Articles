using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Blocks.EntityFrameworkCore
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

				public static ValueConverter<IReadOnlyList<T>, string> BuildJsonReadOnlyListConvertor<T>()
				{
						Func<IReadOnlyList<T>, string> serializeFunc = v => JsonSerializer.Serialize(v);
						Func<string, IReadOnlyList<T>> deserializeFunc = v => JsonSerializer.Deserialize<IReadOnlyList<T>>(v ?? "[]");

						return new ValueConverter<IReadOnlyList<T>, string>(
								v => serializeFunc(v),
								v => deserializeFunc(v));
				}

				public static ValueConverter<TCollection, string> BuildJsonListConvertor<TCollection>()
						//where TCollection : ICollection
				{
						Func<TCollection, string> serializeFunc = v => JsonSerializer.Serialize(v);
						Func<string, TCollection> deserializeFunc = v => JsonSerializer.Deserialize<TCollection>(v ?? "[]");

						return new ValueConverter<TCollection, string>(
								v => serializeFunc(v),
								v => deserializeFunc(v));
				}

				public static PropertyBuilder<IReadOnlyList<T>> HasJsonReadOnlyListConversion<T>(this PropertyBuilder<IReadOnlyList<T>> builder)
				{
						return builder.HasConversion(BuildJsonReadOnlyListConvertor<T>());
				}
				public static PropertyBuilder<T> HasJsonCollectionConversion<T>(this PropertyBuilder<T> builder)
				{
						return builder.HasConversion(BuildJsonListConvertor<T>());
				}

				public static ComplexTypePropertyBuilder<T> HasJsonListConversion<T>(this ComplexTypePropertyBuilder<T> builder)
				{
						return builder.HasConversion(BuildJsonReadOnlyListConvertor<T>());
				}

				public static PropertyBuilder<TCollection> HasJsonListConversion<TCollection, T>(this PropertyBuilder<TCollection> builder)
						where TCollection : IList<T>
				{
						return builder.HasConversion(BuildJsonListConvertor<TCollection>());
				}
		}
}

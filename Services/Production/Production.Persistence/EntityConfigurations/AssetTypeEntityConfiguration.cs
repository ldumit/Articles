using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;
using Production.Domain.ValueObjects;
using System.Reflection.Emit;
using System.Text.Json;

namespace Production.Persistence.EntityConfigurations;

public class AssetTypeEntityConfiguration : EnumEntityConfiguration<AssetType, Domain.Enums.AssetType>
{
    public override void Configure(EntityTypeBuilder<AssetType> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.DefaultCategoryId);
				builder.Property(e => e.MaxNumber).HasDefaultValue(0);

        //builder.Property(e => e.AllowedFileExtensions).HasJsonListConversion<AllowedFileExtensions, string>();
        builder.Property(e => e.DefaultFileExtension).HasDefaultValue("pdf").IsRequired().HasMaxLength(Constraints.C8);

				//builder.ComplexProperty(
				//	 o => o.AllowedFileExtensions, builder =>
				//	 {
				//			 builder.Property(n => n.Extensions)
				//					 .HasJsonListConversion()
				//					 .HasColumnName(nameof(AssetType.AllowedFileExtensions))
				//					 .IsRequired();
				//	 });

				//builder.Ignore(e => e.AllowedFileExtensions);
				//builder.OwnsOne(e => e.AllowedFileExtensions, b =>
				//{
				//		b.Property(a => a.Extensions)
				//				.HasJsonListConversion()
				//				.IsRequired(); // Use your JSON conversion method to store the list as JSON
				//});

				builder.ComplexProperty(e => e.AllowedFileExtensions, complexBuilder =>
				{
						var convertor = BuilderExtensions.BuildJsonListConvertor<string>();
						complexBuilder.Property(e => e.Extensions)
								.HasConversion(convertor)
								.HasColumnName(nameof(AssetType.AllowedFileExtensions))
								.IsRequired();
				});

				//builder.OwnsOne(e => e.AllowedFileExtensions, builder =>
				//{
				//		// This will create a column for Extensions in the AssetType table
				//		builder.Property(a => a.Extensions)
				//				.HasConversion(
				//						v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
				//						v => JsonSerializer.Deserialize<IReadOnlyList<string>>(v, (JsonSerializerOptions)null) ?? new List<string>())
				//				.HasColumnName(nameof(AssetType.AllowedFileExtensions));
				//				//.IsRequired(); // or IsOptional based on your requirements
				//});
		}
}

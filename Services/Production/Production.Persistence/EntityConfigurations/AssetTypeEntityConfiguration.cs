using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class AssetTypeEntityConfiguration : EnumEntityConfiguration<AssetType, Domain.Enums.AssetType>
{
    public override void Configure(EntityTypeBuilder<AssetType> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.DefaultCategoryId);
				builder.Property(e => e.MaxNumber).HasDefaultValue(0);

        builder.Property(e => e.DefaultFileExtension).HasDefaultValue("pdf").IsRequired().HasMaxLength(Constraints.C8);

				builder.ComplexProperty(e => e.AllowedFileExtensions, builder =>
				{
						var convertor = BuilderExtensions.BuildJsonListConvertor<string>();
						builder.Property(e => e.Extensions)
								.HasConversion(convertor)
								.HasColumnName(builder.Metadata.PropertyInfo!.Name)
								.IsRequired();
				});
		}
}

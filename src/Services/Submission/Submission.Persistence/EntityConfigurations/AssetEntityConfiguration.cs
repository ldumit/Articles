using Blocks.Core;

namespace Submission.Persistence.EntityConfigurations;

internal class AssetEntityConfiguration : AuditedEntityConfiguration<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        base.Configure(builder);

        //talk - simple vs composite indexes
        // we don't need the following indexes because we are always taking the assets based on the articleId
        //entity.HasIndex(e => e.StatusId);
        //entity.HasIndex(e => e.TypeId);

        //builder.Property(e => e.Name).HasMaxLength(MaxLength.C64).IsRequired();
				builder.ComplexProperty(
	         o => o.Name, builder =>
	         {
			         builder.Property(n => n.Value)
					         .HasColumnName(builder.Metadata.PropertyInfo!.Name)
					         .HasMaxLength(MaxLength.C64).IsRequired();
	         });

				//builder.Property(e => e.AssetNumber).HasDefaultValue(0);
				builder.ComplexProperty(
	         o => o.Number, builder =>
	         {
			         builder.Property(n => n.Value)
					         .HasColumnName(builder.Metadata.PropertyInfo!.Name)
                   .HasDefaultValue(0).IsRequired();
	         });

				builder.Property(e => e.State).HasEnumConversion().IsRequired();
        builder.Property(e => e.CategoryId).HasConversion<int>().HasDefaultValue(AssetCategory.Core).IsRequired();
        builder.Property(e => e.Type).HasEnumConversion().IsRequired();

        builder.HasOne(d => d.Article).WithMany(p => p.Assets)
            .HasForeignKey(d => d.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.TypeRef).WithMany()
            .HasForeignKey(e => e.Type)
            .HasPrincipalKey(e => e.Name)
            .IsRequired();

				//builder.OwnsOne(e => e.File);

				//builder.ComplexProperty(e => e.File);

				builder.ComplexProperty(e => e.File, fileBuilder =>
				{
						new FileEntityConfiguration().Configure(fileBuilder);
				});
		}
}

using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Submission.Persistence.EntityConfigurations;

public class FileEntityConfiguration
{
		public void Configure(ComplexPropertyBuilder<Domain.Entities.File> builder)
		{
				builder.Property(e => e.FileServerId).HasMaxLength(Constraints.C64);
				builder.Property(e => e.OriginalName).HasMaxLength(Constraints.C256).HasComment("Original full file name, with extension");
				builder.Property(e => e.Size).HasComment("Size of the file in kilobytes");

				builder.ComplexProperty(
					 o => o.Extension, complexBuilder =>
					 {
								complexBuilder.Property(n => n.Value)
										.HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
										.HasMaxLength(Constraints.C8);
					 });

				builder.ComplexProperty(
					 o => o.Name, complexBuilder =>
					 {
								complexBuilder.Property(n => n.Value)
										.HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
										.HasMaxLength(Constraints.C64).HasComment("Final name of the file after renaming");
					 });
		}
}

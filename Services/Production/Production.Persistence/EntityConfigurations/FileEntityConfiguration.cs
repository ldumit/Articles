using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class FileEntityConfiguration : AuditedEntityConfiguration<Domain.Entities.File>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.File> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.AssetId);

        builder.Property(e => e.FileServerId).HasMaxLength(Constraints.C64);
        builder.Property(e => e.Name).HasMaxLength(Constraints.C64).HasComment("Final name of the file after renaming");
        builder.Property(e => e.OriginalName).HasMaxLength(Constraints.C256).HasComment("Full file name, with extension");
        builder.Property(e => e.Size).HasComment("Size of the file in kilobytes");

        builder.ComplexProperty(
           o => o.Extension, nameBuilder =>
           {
               nameBuilder.Property(n => n.Value)
                   .HasColumnName(nameof(Domain.Entities.File.Extension))
                   .HasMaxLength(Constraints.C8);
           });

        //    builder.ComplexProperty(e => e.Extension, builder =>
        //{
        //		builder.Property(f => f.Value)
        //					 .HasColumnName("Extension")
        //					 .IsRequired();
        //});



        builder.Property(e => e.StatusId).HasConversion<int>();
        builder.Property(e => e.Version);

        builder.HasMany(e => e.FileActions).WithOne(e => e.File)
            .HasForeignKey(e => e.FileId).IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.LatestAction).WithOne(e => e.File)
            .HasForeignKey<FileLatestAction>(e => e.FileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}

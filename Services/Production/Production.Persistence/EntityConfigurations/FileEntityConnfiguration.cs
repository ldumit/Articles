using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class FileEntityConnfiguration : AuditedEntityConfiguration<Domain.Entities.File>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.File> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.AssetId);

        entity.Property(e => e.Extension).HasMaxLength(Constraints.C8);
        entity.Property(e => e.FileServerId).HasMaxLength(Constraints.C64);
        entity.Property(e => e.Name).HasMaxLength(Constraints.C64).HasComment("Final name of the file after renaming");
        entity.Property(e => e.OriginalName).HasMaxLength(Constraints.C256).HasComment("Full file name, with extension");
        entity.Property(e => e.Size).HasComment("Size of the file in kilobytes");
        
        entity.Property(e => e.StatusId).HasConversion<int>();
        entity.Property(e => e.Version);

        entity.HasMany(e => e.FileActions).WithOne(e => e.File)
            .HasForeignKey(e => e.FileId).IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne(e => e.LatestAction).WithOne(e => e.File)
            .HasForeignKey<FileLatestAction>(e => e.FileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}

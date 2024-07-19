using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class FileEntityConnfiguration : AuditedEntityConfigurationBase<Domain.Entities.File>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.File> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.AssetId);

        entity.Property(e => e.Extension).HasMaxLength(Constraints.Ten);
        entity.Property(e => e.FileServerId).HasMaxLength(Constraints.Fifty);
        entity.Property(e => e.Name).HasMaxLength(Constraints.Fifty).HasComment("Final name of the file after renaming");
        entity.Property(e => e.OriginalName).HasMaxLength(Constraints.TwoHundred).HasComment("Full file name, with extension");
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

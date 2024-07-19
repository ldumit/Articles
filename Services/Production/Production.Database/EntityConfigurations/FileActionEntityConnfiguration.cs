using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class FileActionEntityConnfiguration : EntityConfigurationBase<FileAction>
{
    public override void Configure(EntityTypeBuilder<FileAction> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.FileId);

        entity.Property(e => e.Comment);
        entity.Property(e => e.FileId);
        entity.Property(e => e.TypeId);

        //entity.HasOne(d => d.User).WithMany(p => p.FileActions)
        //    .HasForeignKey(d => d.UserId)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .HasConstraintName("fileAction_userId_fkey");

        entity.HasOne(d => d.File).WithMany(p => p.FileActions)
            .HasForeignKey(d => d.FileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

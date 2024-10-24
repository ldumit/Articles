using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class FileLatestActionEntityConfiguration : IEntityTypeConfiguration<FileLatestAction>
{
    public void Configure(EntityTypeBuilder<FileLatestAction> entity)
    {
        entity.HasKey(e => e.FileId);
        entity.HasIndex(e => e.ActionId).IsUnique();

        entity.HasOne(e => e.Action).WithOne()
            .HasForeignKey<FileLatestAction>(e => e.ActionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

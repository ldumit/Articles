using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class JournalEntityConfiguration : EntityConfiguration<Journal>
{
    public override void Configure(EntityTypeBuilder<Journal> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.Abbreviation).HasMaxLength(Constraints.C8).IsRequired();
        entity.Property(e => e.Name).HasMaxLength(Constraints.C64).IsRequired();
    }
}

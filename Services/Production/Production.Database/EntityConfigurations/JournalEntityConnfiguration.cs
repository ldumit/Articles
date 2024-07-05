using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class JournalEntityConnfiguration : TenantEntityConfigurationBase<Journal>
{
    private readonly int SpaceId;
    public JournalEntityConnfiguration(int _spaceId)
    {
        SpaceId = _spaceId;
    }
    protected override void ConfigureMore(EntityTypeBuilder<Journal> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.Id }).HasName("journal_pkey");

        entity.ToTable("journal");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.Id);
        entity.Property(e => e.Abbreviation);
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            ;
        entity.Property(e => e.ModifiedDate);
        entity.Property(e => e.Name);
        entity.Property(e => e.ShortName);
        entity.Property(e => e.DefaultTypesetterId);
        //entity.HasOne(d => d.DefaultTypesetter).WithMany(p => p.Journal)
        //        .HasForeignKey(d => d.DefaultTypesetter)
        //        .OnDelete(DeleteBehavior.Restrict)
        //        .HasConstraintName("journal_defaultTypesetter_fkey");
    }
}

using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class JournalEntityConnfiguration : EntityConfiguration<Journal>
{
    public override void Configure(EntityTypeBuilder<Journal> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.Abbreviation).HasMaxLength(Constraints.Ten).IsRequired();
        entity.Property(e => e.Name).HasMaxLength(Constraints.Fifty).IsRequired();
        //entity.Property(e => e.ShortName).HasMaxLength(Constraints.Twenty).IsRequired();
        
        entity.HasOne(e => e.DefaultTypesetter).WithMany()
            .HasForeignKey(e => e.DefaultTypesetterId)
            .HasPrincipalKey(e=> e.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

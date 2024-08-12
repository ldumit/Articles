using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class UserEntityConnfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.FirstName).HasMaxLength(Constraints.C64).IsRequired();
        entity.Property(e => e.LastName).HasMaxLength(Constraints.C64).IsRequired();
        entity.Property(e => e.Title).HasMaxLength(Constraints.C64);
        entity.Property(e => e.Email).HasMaxLength(Constraints.C256).IsRequired();
    }
}

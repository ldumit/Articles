using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class UserEntityConnfiguration : EntityConfigurationBase<User>
{
    public override void Configure(EntityTypeBuilder<User> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.FirstName).HasMaxLength(Constraints.Fifty).IsRequired();
        entity.Property(e => e.LastName).HasMaxLength(Constraints.Fifty).IsRequired();
        entity.Property(e => e.Title).HasMaxLength(Constraints.Fifty);
        entity.Property(e => e.Email).HasMaxLength(Constraints.TwoHundred).IsRequired();
    }
}

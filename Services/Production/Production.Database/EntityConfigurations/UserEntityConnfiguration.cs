using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class UserEntityConnfiguration : EntityConfigurationBase<User>
{
    protected override void ConfigureMore(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.Id).HasName("user_pkey");

        entity.ToTable("user");

        entity.Property(e => e.Id)
            .ValueGeneratedNever()
            ;
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.FirstName);
        entity.Property(e => e.LastName);
        entity.Property(e => e.MiddleName);
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
    }
}

using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.Database.EntityConfigurations;

public class AuthorEntityConnfiguration : EntityConfigurationBase<Author>
{
    public override void Configure(EntityTypeBuilder<Author> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.ArticleId);
        entity.HasIndex(e => new { e.FirstName, e.LastName });

        entity.Property(e => e.FirstName).HasMaxLength(Constraints.Fifty);
        entity.Property(e => e.FullName).HasMaxLength(Constraints.Fifty);
        entity.Property(e => e.LastName).HasMaxLength(Constraints.Fifty);
        entity.Property(e => e.Email).HasMaxLength(Constraints.TwoHundred);
        entity.Property(e => e.Country).HasMaxLength(Constraints.Fifty);

        //talk - storing enums as int vs string, leasibility
        //entity.Property(e => e.Role).HasConversion<int>().IsRequired();
        entity.Property(o => o.Role)
            .HasDefaultValue(UserRole.AUT)
            .HasConversion(
                role => role.ToString(),
                value => (UserRole)Enum.Parse(typeof(UserRole), value))
            .IsRequired();
        entity.Property(o => o.Role)
            .HasDefaultValue(UserRole.AUT)
            .HasEnumConversion()
            .IsRequired();

        entity.HasOne(e => e.User).WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

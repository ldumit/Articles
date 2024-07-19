using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class AuthorEntityConnfiguration : EntityConfigurationBase<Author>
{
    public override void Configure(EntityTypeBuilder<Author> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.ArticleId);
        entity.HasIndex(e => new { e.FirstName, e.LastName });

        entity.Property(e => e.FirstName).HasMaxLength(Constraints.Fifty).IsRequired();
        entity.Property(e => e.FullName).HasMaxLength(Constraints.Fifty).IsRequired();
        entity.Property(e => e.LastName).HasMaxLength(Constraints.Fifty).IsRequired();
        entity.Property(e => e.Email).HasMaxLength(Constraints.TwoHundred);
        entity.Property(e => e.Country).HasMaxLength(Constraints.Fifty);

        entity.Property(e => e.RoleId).HasConversion<int>().IsRequired();

        entity.HasOne(e => e.User).WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

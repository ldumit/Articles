using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class AuthorEntityConfiguration : EntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> entity)
    {
        base.Configure(entity);

        entity.Property(e => e.Country).HasMaxLength(Constraints.C64);
				entity.Property(e => e.Biography).HasMaxLength(Constraints.C2048);

				//talk - storing enums as int vs string, leasibility
				//entity.Property(e => e.Role).HasConversion<int>().IsRequired();
    }
}

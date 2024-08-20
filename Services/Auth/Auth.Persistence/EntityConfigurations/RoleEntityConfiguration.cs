using Articles.EntityFrameworkCore;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal class RoleEntityConfiguration : EntityConfiguration<Role>
{
		public override void Configure(EntityTypeBuilder<Role> entity)
		{
				base.Configure(entity);

				entity.Property(e => e.Code).HasEnumConversion().HasMaxLength(Constraints.C64).IsRequired();
				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				base.SeedFromFile(entity);
		}
}

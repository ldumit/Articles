using Articles.EntityFrameworkCore;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal class RoleEntityConfiguration : EntityConfiguration<Role>
{
		public override void Configure(EntityTypeBuilder<Role> entity)
		{
				base.Configure(entity);

				entity.Property(e => e.Id).ValueGeneratedOnAdd();
				entity.Property(e => e.Type).HasEnumConversion().HasMaxLength(Constraints.C64).IsRequired();
				entity.Property(e => e.Description).HasMaxLength(Constraints.C256).IsRequired(); ;

				base.SeedFromFile(entity);
		}
}

using Blocks.EntityFrameworkCore;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal class RoleEntityConfiguration : EntityConfiguration<Role>
{
		public override void Configure(EntityTypeBuilder<Role> builder)
		{
				base.Configure(builder);

				builder.Property(e => e.Id).ValueGeneratedOnAdd();
				builder.Property(e => e.Type).HasEnumConversion().HasMaxLength(Constraints.C64).IsRequired();
				builder.Property(e => e.Description).HasMaxLength(Constraints.C256).IsRequired(); ;
		}
}

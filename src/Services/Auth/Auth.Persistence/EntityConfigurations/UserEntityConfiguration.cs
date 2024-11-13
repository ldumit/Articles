using Blocks.EntityFrameworkCore;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal class UserEntityConfiguration : AuditedEntityConfiguration<User>
{
		public override void Configure(EntityTypeBuilder<User> entity)
		{
				base.Configure(entity);

				entity.Property(e => e.FirstName).HasMaxLength(Constraints.C64).IsRequired();
				entity.Property(e => e.LastName).HasMaxLength(Constraints.C64).IsRequired();
				entity.Property(e => e.Gender).HasEnumConversion().HasMaxLength(Constraints.C64).IsRequired();

				entity.Property(e => e.Position).HasMaxLength(Constraints.C64);
				entity.Property(e => e.CompanyName).HasMaxLength(Constraints.C256);
				entity.Property(e => e.PictureUrl).HasMaxLength(Constraints.C2048);

				entity.HasMany(p => p.UserRoles).WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();
				entity.HasMany(p => p.RefreshTokens).WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();
		}
}

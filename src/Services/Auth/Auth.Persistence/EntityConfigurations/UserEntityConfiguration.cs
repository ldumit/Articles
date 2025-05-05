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

				entity.Property(e => e.FirstName).HasMaxLength(MaxLength.C64).IsRequired();
				entity.Property(e => e.LastName).HasMaxLength(MaxLength.C64).IsRequired();
				entity.Property(e => e.Gender).HasEnumConversion().HasMaxLength(MaxLength.C64).IsRequired();

				entity.Property(e => e.Title).HasMaxLength(MaxLength.C32);
				entity.Property(e => e.Position).HasMaxLength(MaxLength.C64);
				entity.Property(e => e.CompanyName).HasMaxLength(MaxLength.C256);
				entity.Property(e => e.Affiliation).HasMaxLength(MaxLength.C512);
				entity.Property(e => e.PictureUrl).HasMaxLength(MaxLength.C2048);

				entity.HasMany(p => p.UserRoles).WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();
				entity.HasMany(p => p.RefreshTokens).WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();
		}
}

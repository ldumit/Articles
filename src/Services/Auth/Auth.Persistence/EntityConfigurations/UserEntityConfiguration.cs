using Auth.Domain.Users.ValueObjects;

namespace Auth.Persistence.EntityConfigurations;

internal class UserEntityConfiguration : AuditedEntityConfiguration<User>
{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
				base.Configure(builder);

				builder.Property(e => e.FirstName).HasMaxLength(MaxLength.C64).IsRequired();
				builder.Property(e => e.LastName).HasMaxLength(MaxLength.C64).IsRequired();
				builder.Property(e => e.Gender).HasEnumConversion().HasMaxLength(MaxLength.C64).IsRequired();

				// OwnsOne istead of ComplexProperty because EF.Core doesnt support yet optional proparties with ComplexProperty
				builder.OwnsOne(
						vo => vo.Honorific, b =>
						{
								b.Property(e => e.Value).HasMaxLength(MaxLength.C32).HasColumnName(nameof(User.Honorific));

								b.WithOwner(); // required to avoid navigation issues
						});


				builder.OwnsOne(u => u.ProfessionalProfile, b =>
				{
						b.Property(e => e.Position).HasMaxLength(MaxLength.C32).HasColumnName(nameof(ProfessionalProfile.Position));
						b.Property(e => e.CompanyName).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();
						b.Property(e => e.Affiliation).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();

						b.WithOwner(); // required to avoid navigation issues
				});

				builder.Property(e => e.PictureUrl).HasMaxLength(MaxLength.C2048);

				builder.HasMany(p => p.UserRoles).WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();
				builder.HasMany(p => p.RefreshTokens).WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();
		}
}

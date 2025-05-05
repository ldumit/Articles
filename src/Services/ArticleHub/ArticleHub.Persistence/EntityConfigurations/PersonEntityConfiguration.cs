using ArticleHub.Domain.Entities;
using Blocks.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleHub.Persistence.EntityConfigurations;

public class PersonEntityConfiguration : EntityConfiguration<Person>
{
		protected override bool HasGeneratedId => false;

		public override void Configure(EntityTypeBuilder<Person> entity)
    {
        base.Configure(entity);

				entity.HasIndex(x => x.UserId).IsUnique();

        //talk about EF Core inheritance
				//entity.HasDiscriminator(e => e.TypeDiscriminator)
				//		.HasValue<Person>(nameof(Person))
				//		.HasValue<Author>(nameof(Author))
				//		.HasValue<Typesetter>(nameof(Typesetter));

				entity.Property(e => e.UserId).IsRequired(false);
				entity.Property(e => e.FirstName).HasMaxLength(MaxLength.C64).IsRequired();
        entity.Property(e => e.LastName).HasMaxLength(MaxLength.C64).IsRequired();
        entity.Property(e => e.Title).HasMaxLength(MaxLength.C64);
        entity.Property(e => e.Email).HasMaxLength(MaxLength.C256).IsRequired();

				//entity.HasOne(p => p.User)
				//		.WithOne(u => u.Person)
				//		.HasForeignKey<Person>(p => p.UserId)
				//		.OnDelete(DeleteBehavior.SetNull);
		}
}

using Articles.EntityFrameworkCore;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class PersonEntityConfiguration : EntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> entity)
    {
        base.Configure(entity);

				entity.HasIndex(x => x.UserId).IsUnique();

        //talk about EF Core inheritance
				entity.HasDiscriminator(e => e.PersonType)
						.HasValue<Person>(nameof(Person))
						.HasValue<Author>(nameof(Author))
						.HasValue<Typesetter>(nameof(Typesetter));

				entity.Property(e => e.UserId).IsRequired(false);
				entity.Property(e => e.FirstName).HasMaxLength(Constraints.C64).IsRequired();
        entity.Property(e => e.LastName).HasMaxLength(Constraints.C64).IsRequired();
        entity.Property(e => e.Title).HasMaxLength(Constraints.C64);
        entity.Property(e => e.Email).HasMaxLength(Constraints.C256).IsRequired();

				//entity.HasOne(p => p.User)
				//		.WithOne(u => u.Person)
				//		.HasForeignKey<Person>(p => p.UserId)
				//		.OnDelete(DeleteBehavior.SetNull);
		}
}

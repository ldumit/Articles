using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class PersonEntityConfiguration : EntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

				builder.HasIndex(x => x.UserId).IsUnique();

				//talk about EF Core inheritance
				builder.HasDiscriminator(e => e.TypeDiscriminator)
						.HasValue<Person>(nameof(Person))
						.HasValue<Author>(nameof(Author));

				builder.Property(e => e.UserId).IsRequired(false);
				builder.Property(e => e.FirstName).HasMaxLength(Constraints.C64).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(Constraints.C64).IsRequired();
        builder.Property(e => e.Title).HasMaxLength(Constraints.C64);

				builder.ComplexProperty(
					 o => o.Email, builder =>
					 {
							 builder.Property(n => n.Value)
									 .HasColumnName(builder.Metadata.PropertyInfo!.Name)
									 .HasMaxLength(Constraints.C64).HasComment("Final name of the file after renaming");
					 });
		}
}

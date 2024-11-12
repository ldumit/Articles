using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
				builder.Property(e => e.Affiliation).IsRequired().HasMaxLength(Constraints.C512)
						.HasComment("Institution or organization they are associated with when they conduct their research.");
    }
}

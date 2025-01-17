namespace Review.Persistence.EntityConfigurations;

public class ReviewerEntityConfiguration : IEntityTypeConfiguration<Reviewer>
{
    public void Configure(EntityTypeBuilder<Reviewer> builder)
    {        				
				builder.Property(e => e.Affiliation).IsRequired().HasMaxLength(Constraints.C512)
						.HasComment("Institution or organization they are associated with when they conduct their research.");

				builder.HasMany(e => e.Specializations).WithOne()
						.IsRequired()
						.OnDelete(DeleteBehavior.NoAction);

		}
}

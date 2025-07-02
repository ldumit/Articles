namespace Review.Persistence.EntityConfigurations;

public class ReviewerEntityConfiguration : IEntityTypeConfiguration<Reviewer>
{
    public void Configure(EntityTypeBuilder<Reviewer> builder)
    {
				builder.HasBaseType<Person>();

				// talk - many to many relation from the Domain POV
				// talk - the property is called Specialization but here we are talking abiout tables so ReviewerJournal is a better name
				//builder.HasMany(r => r.Specializations).WithMany()
				//		.UsingEntity(j => j.ToTable("ReviewerJournal"));

				//builder.HasMany(r => r.Specializations).WithMany();

				builder.HasMany(r => r.Specializations).WithOne(j => j.Reviewer);

		}
}

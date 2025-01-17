namespace Review.Persistence.EntityConfigurations;

public class JournalEntityConfiguration : EntityConfiguration<Journal>
{
		public override void Configure(EntityTypeBuilder<Journal> builder)
		{
				base.Configure(builder);

				builder.Property(e => e.Abbreviation).HasMaxLength(Constraints.C8).IsRequired();
				builder.Property(e => e.Name).HasMaxLength(Constraints.C64).IsRequired();

				builder.HasOne(e => e.ChiefEditor).WithMany()
						.HasForeignKey(e => e.ChiefEditorId)
						.HasPrincipalKey(e => e.Id)
						.OnDelete(DeleteBehavior.Restrict);
		}
}

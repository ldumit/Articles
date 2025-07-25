﻿namespace Review.Persistence.EntityConfigurations;

public class JournalEntityConfiguration : EntityConfiguration<Journal>
{
		protected override bool HasGeneratedId => false;

		public override void Configure(EntityTypeBuilder<Journal> builder)
		{
				base.Configure(builder);

				builder.Property(e => e.Abbreviation).HasMaxLength(MaxLength.C8).IsRequired();
				builder.Property(e => e.Name).HasMaxLength(MaxLength.C64).IsRequired();


				builder.HasMany(r => r.Reviewers).WithOne(j => j.Journal);

				//builder.HasOne(e => e.ChiefEditor).WithMany()
				//		.HasForeignKey(e => e.ChiefEditorId)
				//		.HasPrincipalKey(e => e.Id)
				//		.OnDelete(DeleteBehavior.Restrict);
		}
}

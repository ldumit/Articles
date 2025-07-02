namespace Review.Persistence.EntityConfigurations;

public class JournalEditorEntityConfiguration : IEntityTypeConfiguration<JournalEditor>
{
    public void Configure(EntityTypeBuilder<JournalEditor> builder)
		{
				builder.HasKey(je => new { je.JournalId, je.EditorId });

				//builder
				//		.HasOne(je => je.Journal)
				//		.WithMany(j => j.Editors)
				//		.HasForeignKey(je => je.JournalId);

				//builder
				//		.HasOne(je => je.Editor)
				//		.WithMany(e => e.JournalEditors)
				//		.HasForeignKey(je => je.EditorId);
		}
}

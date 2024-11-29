namespace Submission.Persistence.EntityConfigurations;
public class ArticleEntityConfiguration : AuditedEntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => e.Title);

        //talk - using constants instead of direct numbers
        entity.Property(e => e.Title).HasMaxLength(Constraints.C256).IsRequired();
        entity.Property(e => e.Stage).HasEnumConversion().IsRequired();
				entity.Property(e => e.Type).HasEnumConversion().IsRequired();
				entity.Property(e => e.Scope).HasMaxLength(Constraints.C2048).IsRequired();

				entity.HasOne<Stage>().WithMany()
					 .HasForeignKey(e => e.Stage)
					 .HasPrincipalKey(e => e.Name)
					 .IsRequired()
					 .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.SubmittedBy).WithMany()
            .HasForeignKey(e => e.SubmittedById)
            .HasPrincipalKey(e => e.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Journal).WithMany(e => e.Articles)
            .HasForeignKey(e => e.JournalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(e => e.Assets).WithOne(e => e.Article)
            .HasForeignKey(e => e.ArticleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.StageHistories).WithOne()
            .HasForeignKey(e => e.ArticleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

				entity.HasMany(e => e.Actions).WithOne()
		        .HasForeignKey(e => e.EntityId)
		        .IsRequired()
		        .OnDelete(DeleteBehavior.Cascade);
		}
}

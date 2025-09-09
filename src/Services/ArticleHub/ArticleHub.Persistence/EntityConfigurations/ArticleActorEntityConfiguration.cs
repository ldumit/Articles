namespace ArticleHub.Persistence.EntityConfigurations;

internal class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
{
		public void Configure(EntityTypeBuilder<ArticleActor> builder)
		{
				builder.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });
				//entity.HasKey(e => e.ArticleId);
				builder.Property(e => e.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

				builder.HasOne<Article>()
						.WithMany(a => a.Actors)
						.HasForeignKey(aa => aa.ArticleId)
						.OnDelete(DeleteBehavior.Cascade);

				builder.HasOne(a => a.Person)
						.WithMany()
						.HasForeignKey(aa => aa.PersonId)
						.OnDelete(DeleteBehavior.Restrict);
		}
}

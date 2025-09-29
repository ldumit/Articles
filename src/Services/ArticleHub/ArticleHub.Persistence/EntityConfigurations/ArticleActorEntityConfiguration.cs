namespace ArticleHub.Persistence.EntityConfigurations;

internal class ArticleActorEntityConfiguration : EntityConfiguration<ArticleActor>
{
		public override void Configure(EntityTypeBuilder<ArticleActor> builder)
		{
				builder.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });
				builder.Property(e => e.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

				builder.HasOne(a => a.Person)
						.WithMany()
						.HasForeignKey(aa => aa.PersonId)
						.OnDelete(DeleteBehavior.Restrict);
		}
}

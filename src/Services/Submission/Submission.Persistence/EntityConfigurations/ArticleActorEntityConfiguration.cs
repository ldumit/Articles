using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;
using Articles.EntityFrameworkCore;
using Articles.Security;

namespace Submission.Persistence.EntityConfigurations;

internal class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
{
		public void Configure(EntityTypeBuilder<ArticleActor> builder)
		{
				builder.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });
				builder.Property(e => e.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

				//talk about EF Core inheritance
				builder.HasDiscriminator(e => e.ActorType)
						.HasValue<ArticleActor>(nameof(ArticleActor))
						.HasValue<AuthorActor>(nameof(AuthorActor));

				builder.HasOne(aa => aa.Article)
						.WithMany(a => a.Actors)
						.HasForeignKey(aa => aa.ArticleId)
						.OnDelete(DeleteBehavior.Cascade);

				builder.HasOne(aa => aa.Person)
						.WithMany(a => a.ArticleActors)
						.HasForeignKey(aa => aa.PersonId)
						.OnDelete(DeleteBehavior.Restrict);
		}
}

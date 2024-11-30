using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Blocks.EntityFrameworkCore;
using Articles.Security;
using ArticleHub.Domain.Entities;

namespace ArticleHub.Persistence.EntityConfigurations;

internal class ArticleContributorEntityConfiguration : IEntityTypeConfiguration<ArticleContributor>
{
		public void Configure(EntityTypeBuilder<ArticleContributor> entity)
		{
				entity.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });
				//entity.HasKey(e => e.ArticleId);
				entity.Property(e => e.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

				entity.HasOne<Article>()
						.WithMany(a => a.Contributors)
						.HasForeignKey(aa => aa.ArticleId)
						.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(aa => aa.Person)
						.WithMany()
						.HasForeignKey(aa => aa.PersonId)
						.OnDelete(DeleteBehavior.Restrict);
		}
}

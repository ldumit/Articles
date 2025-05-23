﻿using Articles.Security;

namespace Review.Persistence.EntityConfigurations;

internal class ArticleContributorEntityConfiguration : IEntityTypeConfiguration<ArticleContributor>
{
		public void Configure(EntityTypeBuilder<ArticleContributor> builder)
		{
				builder.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });
				builder.Property(e => e.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

				//talk about EF Core inheritance
				builder.HasDiscriminator(e => e.TypeDiscriminator)
						.HasValue<ArticleContributor>(nameof(ArticleContributor))
						.HasValue<ArticleAuthor>(nameof(ArticleAuthor));

				builder.HasOne(aa => aa.Article)
						.WithMany(a => a.Contributors)
						.HasForeignKey(aa => aa.ArticleId)
						.OnDelete(DeleteBehavior.Cascade);

				builder.HasOne(aa => aa.Person)
						.WithMany(a => a.ArticleContributors)
						.HasForeignKey(aa => aa.PersonId)
						.OnDelete(DeleteBehavior.Restrict);
		}
}

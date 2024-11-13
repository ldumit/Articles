﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;
using Blocks.EntityFrameworkCore;
using Blocks.Security;

namespace Production.Persistence.EntityConfigurations;

internal class ArticleContributorEntityConfiguration : IEntityTypeConfiguration<ArticleContributor>
{
		public void Configure(EntityTypeBuilder<ArticleContributor> entity)
		{
				entity.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });
				//entity.HasKey(e => e.ArticleId);
				entity.Property(e => e.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

				entity.HasOne(aa => aa.Article)
						.WithMany(a => a.Contributors)
						.HasForeignKey(aa => aa.ArticleId)
						.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(aa => aa.Person)
						.WithMany(a => a.ArticleContributors)
						.HasForeignKey(aa => aa.PersonId)
						.OnDelete(DeleteBehavior.Restrict);
		}
}

using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;
using Submission.Domain.Enums;

namespace Submission.Persistence.EntityConfigurations;

public class ArticleAuthorEntityConfiguration : IEntityTypeConfiguration<AuthorActor>
{
    public void Configure(EntityTypeBuilder<AuthorActor> entity)
    {
        //entity.Property(e => e.ContributionAreas).HasJsonListConversion<List<ContributionArea>, ContributionArea>().IsRequired();
				entity.Property(e => e.ContributionAreas).HasJsonCollectionConversion().IsRequired();
		}
}

using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Blocks.Mapster;
using ArticleHub.Persistence;
using Articles.IntegrationEvents.Contracts.Articles;
using ArticleHub.Domain.Entities;

namespace ArticleHub.API.Articles.Consumers;

public class ArticleSubmittedConsumer(ArticleHubDbContext _dbContext) : IConsumer<ArticleApprovedForReviewEvent>
{
		public async Task Consume(ConsumeContext<ArticleApprovedForReviewEvent> context)
		{
				var articleDto = context.Message.Article;

				var journal = await _dbContext.Journals.FirstOrDefaultAsync(j => j.Id == articleDto.Journal.Id);
				if (journal == null)
				{
						journal = articleDto.Journal.Adapt<Journal>();
						_dbContext.Journals.Add(journal);
				}

				var article = articleDto.AdaptWith<Article>(dest =>
				{
						dest.Journal = journal;
						dest.SubmittedById = articleDto.SubmittedBy.Id;
				});

				foreach (var contributorDto in articleDto.Actors)
				{
						var contributor = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == contributorDto.Person.Id);
						if (contributor == null)
						{
								contributor = contributorDto.Person.Adapt<Person>();
								_dbContext.Persons.Add(contributor);
						}

						article.Contributors.Add(
								new ArticleContributor { ArticleId = article.Id, PersonId = contributor.Id, Role = contributorDto.Role });
				}

				_dbContext.Articles.Add(article);

				await _dbContext.SaveChangesAsync();
		}
}

using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Blocks.Mapster;
using ArticleHub.Persistence;
using Articles.Abstractions.Events;
using ArticleHub.Domain.Entities;

namespace ArticleHub.Application.Features.Articles.EventHandlers;

public class ArticleSubmittedEventHandler(ArticleHubDbContext _dbContext) : IConsumer<ArticleSubmittedEvent>
{
		public async Task Consume(ConsumeContext<ArticleSubmittedEvent> context)
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

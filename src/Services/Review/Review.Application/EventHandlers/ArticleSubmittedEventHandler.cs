using MassTransit;
using Microsoft.EntityFrameworkCore;
using Blocks.Mapster;
using Articles.Abstractions.Events;
using Review.Persistence;

namespace Review.Application.Features.Articles.EventHandlers;

public class ArticleSubmittedEventHandler(ReviewDbContext _dbContext) : IConsumer<ArticleSubmittedEvent>
{
		public async Task Consume(ConsumeContext<ArticleSubmittedEvent> context)
		{
				var articleDto = context.Message.Article;

				//find or create journal
				var journal = await _dbContext.Journals.FirstOrDefaultAsync(j => j.Id == articleDto.Journal.Id);
				if (journal is null)
				{
						journal = articleDto.Journal.Adapt<Journal>();
						_dbContext.Journals.Add(journal);
				}

				var article = articleDto.AdaptWith<Article>(dest =>
				{
						//dest.Journal = journal;
						dest.SubmittedById = articleDto.SubmittedBy.Id;
				});


				//create contributors
				foreach (var contributorDto in articleDto.Actors)
				{
						var contributor = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == contributorDto.Person.Id);
						if (contributor is null)
						{
								contributor = contributorDto.Person.Adapt<Person>();
								_dbContext.Persons.Add(contributor);
						}

						article.AssignActor(contributor.Id, contributorDto.Role);
				}

				_dbContext.Articles.Add(article);

				await _dbContext.SaveChangesAsync();
		}

}

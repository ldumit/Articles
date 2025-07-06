using MassTransit;
using Articles.Abstractions.Events;
using Review.Persistence;
using Auth.Grpc;
using Blocks.Domain;

namespace Review.Application.Features.Articles.EventHandlers;

public class ArticleSubmittedEventHandler(PersonRepository _personRepository, Repository<Journal> _journalRepository, ReviewDbContext _dbContext, IPersonService _personService) : IConsumer<ArticleSubmittedEvent>
{
		public async Task Consume(ConsumeContext<ArticleSubmittedEvent> context)
		{
				var articleDto = context.Message.Article;

				//create actors
				var actors = new List<ArticleActor>();
				foreach (var actorDto in articleDto.Actors)
				{
						var person = await _personRepository.GetByIdAsync(actorDto.Person.Id);
						if (person is null)
						{
								var response = await _personService.GetPersonByIdAsync(new GetPersonRequest { PersonId = actorDto.Person.Id }, context.CancellationToken);
								var personInfo = response.PersonInfo;

								switch (actorDto.Role)
								{
										case UserRoleType.AUT:
												person = personInfo.Adapt<Author>(); break;
										case UserRoleType.REVED:
												person = personInfo.Adapt<Editor>(); break;
										default:
												throw new DomainException($"Unknow role for {personInfo.Email}");
								}
								actors.Add(new ArticleActor { PersonId = person.Id });
								await _personRepository.AddAsync(person);
						}

						//article.AssignActor(actor.Id, actorDto.Role);
				}

				//create actors
				var assets = new List<Asset>();
				foreach (var assetDto in articleDto.Assets)
				{
						//todo create assets & files, then download & upload files) 

				}

				//find or create journal
				var journal = await  _journalRepository.FindByIdAsync(articleDto.Journal.Id);
				if (journal is null)
				{
						journal = articleDto.Journal.Adapt<Journal>();
						_dbContext.Journals.Add(journal);
				}

				var article = Article.AcceptSubmitted(articleDto, actors, assets);


				_dbContext.Articles.Add(article);

				await _dbContext.SaveChangesAsync();
		}

}

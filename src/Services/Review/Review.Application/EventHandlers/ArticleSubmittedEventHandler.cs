using MassTransit;
using Articles.Abstractions.Events;
using Review.Persistence;
using Auth.Grpc;
using Blocks.Domain;

namespace Review.Application.Features.Articles.EventHandlers;

public class ArticleSubmittedEventHandler(
		PersonRepository _personRepository, 
		Repository<Journal> _journalRepository, 
		ReviewDbContext _dbContext,
		AssetTypeRepository _assetTypeRepository,
		IPersonService _personService) : IConsumer<ArticleSubmittedEvent>
{
		public async Task Consume(ConsumeContext<ArticleSubmittedEvent> context)
		{
				var articleDto = context.Message.Article;

				//create actors
				var actors = new List<ArticleActor>();
				foreach (var actorDto in articleDto.Actors)
				{
						var person = await _personRepository.GetByIdAsync(actorDto.Person.Id);
						ArticleActor actor = default!;
						//if (person is null)
						{
								//var response = await _personService.GetPersonByIdAsync(new GetPersonRequest { PersonId = actorDto.Person.Id }, context.CancellationToken);
								//var personInfo = response.PersonInfo;
								if (actorDto.Role == UserRoleType.AUT || actorDto.Role == UserRoleType.CORAUT)
								{
										if(person is null)
												person = actorDto.Person.Adapt<Author>();

										actor = new ArticleAuthor { 
												PersonId = person.Id, 
												Person = person,
												Role = actorDto.Role, 
												ContributionAreas = actorDto.ContributionAreas
										};
								}
								else if(actorDto.Role == UserRoleType.REVED)
								{
										if (person is null)
												person = actorDto.Person.Adapt<Editor>();

										actor = new ArticleActor { 
												PersonId = person.Id, 
												Person = person,
												Role = actorDto.Role 
										};
								}
								else
								{
										// decide : or ignore, just log a warning
										throw new DomainException($"Unknow role for {actorDto.Person.Email}");
								}
						}
						actors.Add(actor);
						//await _personRepository.AddAsync(person);

						//article.AssignActor(actor.Id, actorDto.Role);
				}

				//create actors
				var assets = new List<Asset>();
				foreach (var assetDto in articleDto.Assets)
				{
						var assetTypeDefinition = _assetTypeRepository.GetById(assetDto.Type);

						var asset = Asset.CreateFromSubmission(assetDto, assetTypeDefinition, articleDto.Id);

						//todo create assets & files, then download & upload files) 

						assets.Add(asset);
				}

				//find or create journal
				var journal = await  _journalRepository.FindByIdAsync(articleDto.Journal.Id);
				if (journal is null)
				{
						journal = articleDto.Journal.Adapt<Journal>();
						await _journalRepository.AddAsync(journal);
				}

				var article = Article.AcceptSubmitted(articleDto, actors, assets);
				journal.AddArticle(article);

				//_dbContext.Articles.Add(article);

				await _dbContext.SaveChangesAsync();
		}

}

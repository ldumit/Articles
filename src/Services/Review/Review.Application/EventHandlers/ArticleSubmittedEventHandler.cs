using Articles.Abstractions.Events;
using Blocks.Domain;
using FileStorage.Contracts;
using MassTransit;
using Review.Application.FileStorage;
using Review.Domain.Shared;
using Review.Persistence;

namespace Review.Application.Features.Articles.EventHandlers;

public class ArticleSubmittedEventHandler(
		ReviewDbContext _dbContext,
		PersonRepository _personRepository, 
		Repository<Journal> _journalRepository, 
		AssetTypeRepository _assetTypeRepository,
		IFileService reviewFileService,
		FileServiceFactory fileServiceFactory) 
		: IConsumer<ArticleSubmittedEvent>
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
								// decide : throw or ignore, just log a warning
								throw new DomainException($"Unknow role for {actorDto.Person.Email}");
						}
						actors.Add(actor);
				}

				var submissionFileService = fileServiceFactory(FileStorageType.Submission);

				//create actors
				var assets = new List<Asset>();
				foreach (var assetDto in articleDto.Assets)
				{
						var assetTypeDefinition = _assetTypeRepository.GetById(assetDto.Type);

						var asset = Asset.CreateFromSubmission(assetDto, assetTypeDefinition, articleDto.Id);

						var(fileStream, fileMetada) = await submissionFileService.DownloadAsync(asset.File.FileServerId, context.CancellationToken);

						var fileMetadata = await reviewFileService.UploadAsync(
								new FileUploadRequest(fileMetada.StoragePath, fileMetada.FileName, fileMetada.ContentType, fileMetada.FileSize), 
								fileStream);

						asset.CreateFile(fileMetadata, assetTypeDefinition);

						assets.Add(asset);
				}

				//find or create journal
				var journal = await  _journalRepository.FindByIdAsync(articleDto.Journal.Id);
				if (journal is null)
				{
						journal = articleDto.Journal.Adapt<Journal>();
						await _journalRepository.AddAsync(journal);
				}

				//create article
				var article = Article.AcceptSubmitted(articleDto, actors, assets);
				journal.AddArticle(article);

				await _dbContext.SaveChangesAsync();
		}

}

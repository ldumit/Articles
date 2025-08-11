using Articles.Abstractions.Events;
using Articles.Abstractions.Events.Dtos;
using Blocks.Domain;
using FileStorage.Contracts;
using MassTransit;
using Review.Application.FileStorage;
using Review.Domain.Assets;
using Review.Domain.Shared;

namespace Review.Application.Features.Articles.InitializeFromSubmission;

public class ArticleApprovedForReviewEventHandler(
		ReviewDbContext _dbContext,
		ArticleRepository _articleRepository,
		PersonRepository _personRepository, 
		Repository<Journal> _journalRepository, 
		AssetTypeRepository _assetTypeRepository,
		IFileService reviewFileService,
		FileServiceFactory fileServiceFactory) 
		: IConsumer<ArticleApprovedForReviewEvent>
{
		public async Task Consume(ConsumeContext<ArticleApprovedForReviewEvent> context)
		{
				var articleDto = context.Message.Article;

				//talk - inbox pattern vs simple business rules(chek if the artile already exists)
				await _articleRepository.ExistsOrThrowAsync(articleDto.Id, context.CancellationToken);
				
				var actors = await CreateActors(articleDto);

				var assets = await CreateAssets(articleDto, context.CancellationToken);

				var journal = await GetOrCreateJournal(articleDto);

				//create article
				var article = Article.AcceptSubmitted(articleDto, actors, assets);
				journal.AddArticle(article);

				await _dbContext.SaveChangesAsync();
		}

		private async Task<Journal> GetOrCreateJournal(ArticleDto articleDto)
		{
				var journal = await _journalRepository.FindByIdAsync(articleDto.Journal.Id);
				if (journal is null)
				{
						journal = articleDto.Journal.Adapt<Journal>();
						await _journalRepository.AddAsync(journal);
				}

				return journal;
		}

		private async Task<List<Asset>> CreateAssets(ArticleDto articleDto, CancellationToken ct = default)
		{
				var submissionFileService = fileServiceFactory(FileStorageType.Submission);

				//create actors
				var assets = new List<Asset>();
				foreach (var assetDto in articleDto.Assets)
				{
						var assetTypeDefinition = _assetTypeRepository.GetById(assetDto.Type);

						var asset = Asset.CreateFromSubmission(assetDto, assetTypeDefinition, articleDto.Id);

						var (fileStream, fileMetada) = await submissionFileService.DownloadAsync(asset.File.FileServerId, ct);

						var fileMetadata = await reviewFileService.UploadAsync(
								new FileUploadRequest(fileMetada.StoragePath, fileMetada.FileName, fileMetada.ContentType, fileMetada.FileSize),
								fileStream);

						asset.CreateFile(fileMetadata, assetTypeDefinition);

						assets.Add(asset);
				}

				return assets;
		}

		private async Task<List<ArticleActor>> CreateActors(ArticleDto articleDto)
		{
				//create actors
				var actors = new List<ArticleActor>();
				foreach (var actorDto in articleDto.Actors)
				{
						var person = await _personRepository.GetByIdAsync(actorDto.Person.Id);
						ArticleActor actor = default!;
						if (actorDto.Role == UserRoleType.AUT || actorDto.Role == UserRoleType.CORAUT)
						{
								if (person is null)
										person = actorDto.Person.Adapt<Author>();

								actor = new ArticleAuthor(actorDto.ContributionAreas)
								{
										PersonId = person.Id,
										Person = person,
										Role = actorDto.Role,
								};
						}
						else if (actorDto.Role == UserRoleType.REVED)
						{
								if (person is null)
										person = actorDto.Person.Adapt<Editor>();

								actor = new ArticleActor
								{
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

				return actors;
		}
}

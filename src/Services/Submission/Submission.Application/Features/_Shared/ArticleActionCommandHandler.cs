using Submission.Domain.StateMachines;

namespace Submission.Application.Features.Shared;

public abstract class ArticleActionCommandHandler<TCommand>
		: IRequestHandler<TCommand, IdResponse>
		where TCommand : ArticleCommand
{
		protected readonly ArticleRepository _articleRepository;
		protected readonly ArticleStateMachineFactory _stateMachineFactory;

		protected Article? _article;

    protected ArticleActionCommandHandler(ArticleRepository articleRepository, ArticleStateMachineFactory stateMachineFactory)
				=> (_articleRepository, _stateMachineFactory) = (articleRepository, stateMachineFactory);

    public async Task<IdResponse> Handle(TCommand command, CancellationToken cancellationToken)
		{
				_article = await _articleRepository.FindByIdOrThrowAsync(command.ArticleId);
				
				_article.SetStage(NextStage, command, _stateMachineFactory);
				
				await _articleRepository.SaveChangesAsync();

				return new IdResponse(_article.Id);
		}

		protected virtual ArticleStage NextStage => _article!.Stage;
}

namespace Submission.Application.Features.ApproveArticle;

public class ApproveArticleCommandHandler(ArticleRepository _articleRepository, ArticleStateMachineFactory _stateMachineFactory)
		: IRequestHandler<ApproveArticleCommand, IdResponse>
{
		public async Task<IdResponse> Handle(ApproveArticleCommand command, CancellationToken cancellationToken)
		{
				var article = await _articleRepository.FindByIdOrThrowAsync(command.ArticleId);
				
				article.Approve(command, _stateMachineFactory);
				
				await _articleRepository.SaveChangesAsync();

				return new IdResponse(article.Id);
		}
}

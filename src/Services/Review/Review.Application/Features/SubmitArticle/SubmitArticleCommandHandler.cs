namespace Review.Application.Features.SubmitArticle;

public class SubmitArticleCommandHandler(ArticleRepository _articleRepository, ArticleStateMachineFactory _stateMachineFactory)
		: IRequestHandler<SubmitArticleCommand, IdResponse>
{
		public async Task<IdResponse> Handle(SubmitArticleCommand command, CancellationToken cancellationToken)
		{
				var article = await _articleRepository.FindByIdOrThrowAsync(command.ArticleId);

				article.Submit(command, _stateMachineFactory);

				await _articleRepository.SaveChangesAsync();

				return new IdResponse(article.Id);
		}
}

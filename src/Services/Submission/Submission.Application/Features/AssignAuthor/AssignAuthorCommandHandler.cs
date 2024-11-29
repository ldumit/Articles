namespace Submission.Application.Features.AssignAuthor;

public class AssignAuthorCommandHandler(ArticleRepository _articleRepository)
		: IRequestHandler<AssignAuthorCommand, IdResponse>
{
		public async Task<IdResponse> Handle(AssignAuthorCommand command, CancellationToken cancellationToken)
		{
				var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

				var author = await _articleRepository.Context.Authors.FindByIdOrThrowAsync(command.AuthorId);

				article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor, command);

				await _articleRepository.SaveChangesAsync();

				return new IdResponse(article.Id);
		}
}

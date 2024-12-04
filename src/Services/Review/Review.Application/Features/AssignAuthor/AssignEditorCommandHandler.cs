namespace Review.Application.Features.AssignEditor;

public class AssignEditorCommandHandler(ArticleRepository _articleRepository)
		: IRequestHandler<AssignEditorCommand, IdResponse>
{
		public async Task<IdResponse> Handle(AssignEditorCommand command, CancellationToken cancellationToken)
		{
				var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

				var author = await _articleRepository.Context.Authors.FindByIdOrThrowAsync(command.EditorId);

				///article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor, command);

				await _articleRepository.SaveChangesAsync();

				return new IdResponse(article.Id);
		}
}

namespace Review.Application.Features.Articles.AssignEditor;

public class AssignEditorCommandHandler(ArticleRepository _articleRepository)
        : IRequestHandler<AssignEditorCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AssignEditorCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        var editor = await _articleRepository.Context.Reviewers.FindByIdOrThrowAsync(command.EditorId);

        article.AssignEditor(editor, command);

        await _articleRepository.SaveChangesAsync();

        return new IdResponse(article.Id);
    }
}

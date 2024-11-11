using MediatR;
using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.EntityFrameworkCore;
using Submission.Persistence.Repositories;
using Submission.Domain.StateMachines;

namespace Submission.Application.Features.SubmitArticle;

public class SubmitArticleCommandHandler(ArticleRepository _articleRepository, ArticleStateMachineFactory _stateMachineFactory)
		: IRequestHandler<SubmitArticleCommand, IdResponse>
{
		public async Task<IdResponse> Handle(SubmitArticleCommand command, CancellationToken cancellationToken)
		{
				var article = await _articleRepository.FindByIdOrThrowAsync(command.ArticleId);

				article.SetStage(ArticleStage.Submitted, command, _stateMachineFactory);

				await _articleRepository.SaveChangesAsync();

				return new IdResponse(article.Id);
		}
}

using MassTransit;
using Articles.Abstractions.Events;
using Articles.Abstractions.Events.Dtos;

namespace Submission.Application.Features.ApproveArticle;

public class ApproveArticleCommandHandler(ArticleRepository _articleRepository, ArticleStateMachineFactory _stateMachineFactory, IPublishEndpoint _publishEndpoint)
		: IRequestHandler<ApproveArticleCommand, IdResponse>
{
		public async Task<IdResponse> Handle(ApproveArticleCommand command, CancellationToken cancellationToken)
		{
				var article = await _articleRepository.FindByIdOrThrowAsync(command.ArticleId);
				
				article.Approve(command, _stateMachineFactory);
				
				await _articleRepository.SaveChangesAsync();

				await _publishEndpoint.Publish(new ArticleSubmittedEvent(article.Adapt<ArticleDto>()));

				return new IdResponse(article.Id);
		}
}

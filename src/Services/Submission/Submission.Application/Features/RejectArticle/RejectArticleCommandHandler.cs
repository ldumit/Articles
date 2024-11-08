using Articles.Abstractions;
using Submission.Persistence.Repositories;
using Submission.Application.Features.Shared;
using Submission.Domain.StateMachines;

namespace Submission.Application.Features.RejectArticle;

public class RejectArticleCommandHandler(ArticleRepository articleRepository, ArticleStateMachineFactory stateMachineFactory)
		: ArticleActionCommandHandler<RejectArticleCommand>(articleRepository, stateMachineFactory)
{		
		protected override ArticleStage NextStage => ArticleStage.InitialRejected;
}

using Submission.Domain.StateMachines;

namespace Submission.Application.Features.RejectArticle;

public class RejectArticleCommandHandler(ArticleRepository articleRepository, ArticleStateMachineFactory stateMachineFactory)
		: ArticleActionCommandHandler<RejectArticleCommand>(articleRepository, stateMachineFactory)
{		
		protected override ArticleStage NextStage => ArticleStage.InitialRejected;
}

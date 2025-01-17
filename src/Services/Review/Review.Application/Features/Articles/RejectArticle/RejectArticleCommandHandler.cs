using Review.Application.Features.Articles._Shared;
using Review.Domain.StateMachines;

namespace Review.Application.Features.Articles.RejectArticle;

public class RejectArticleCommandHandler(ArticleRepository articleRepository, ArticleStateMachineFactory stateMachineFactory)
        : ArticleActionCommandHandler<RejectArticleCommand>(articleRepository, stateMachineFactory)
{
    protected override ArticleStage NextStage => ArticleStage.InitialRejected;
}

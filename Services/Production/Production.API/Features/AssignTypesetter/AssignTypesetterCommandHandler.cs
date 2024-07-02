using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.API.Features.AssignTypesetter;

public class AssignTypesetterCommandHandler(IServiceProvider serviceProvider) 
    : BaseCommandHandler<AssignTypesetterCommand, ArticleCommandResponse>(serviceProvider)
{
    //protected readonly WorklfowEventDispatcher _workflowEventService;

    public override async Task<ArticleCommandResponse> Handle(AssignTypesetterCommand command, CancellationToken cancellationToken)
    {
        using var transaction = _articleRepository.BeginTransaction();
        var article = _articleRepository.GetById(command.ArticleId);
        
        ChangeStage(article, command);

        article.Typesetter = _mapper.Map<Typesetter>(command);
        await _articleRepository.SaveChangesAsync();

        //todo - transform into domain event
        await AddTypesetterToDiscussion(command.ArticleId,article);
       
        transaction.Commit();

        //await _workflowEventService.PublishEvent(new StartProductionEndWF1Event { ArticleId = command.ArticleId });
        //await _workflowEventService.PublishEvent(new StartProductionStartWF2Event { ArticleId = command.ArticleId });

        return new ArticleCommandResponse();
    }

    private async Task AddTypesetterToDiscussion(int articleId, Article article)
    {
        //var groups = await _discussionGroupRepository.GetGroupsByArticleIdAsync(articleId);
        //foreach (var group in groups)
        //{
        //    if (!group.Users.Any(x => x.UserId == (int)article.ArticleTypesetter.UserId))
        //        group.Users.Add(new GroupUser { UserId = (int)article.ArticleTypesetter.UserId });
        //}
        //await _discussionGroupRepository.SaveChangesAsync();
    }
    protected override ArticleStagesCode GetNextStage(Article article) => ArticleStagesCode.IN_PRODUCTION;
}

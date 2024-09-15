using Articles.Abstractions;
using Production.Domain.Enums;
using Stateless;

namespace Production.Application.StateMachines;

public class ArticleStateMachine
{
    private StateMachine<ArticleStage, ArticleActionType> _stateMachine;

    public ArticleStateMachine()
    {
        _stateMachine = new StateMachine<ArticleStage, ArticleActionType>(ArticleStage.Accepted);

        _stateMachine.Configure(ArticleStage.Accepted)
                .Permit(ArticleActionType.AssignTypesetter, ArticleStage.InProduction);

        _stateMachine.Configure(ArticleStage.FinalProduction)
                .Permit(ArticleActionType.SchedulePublication, ArticleStage.PublicationScheduled);
    }

    //public void Fire(ActionType action)
    //{
    //		_stateMachine.Fire(action);
    //}
}
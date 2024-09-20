using Articles.Abstractions;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Production.Persistence;
using Stateless;

namespace Production.Application.StateMachines;

public delegate ArticleStateMachine ArticleStateMachineFactory(ArticleStage articleStage);

public class ArticleStateMachine
{
		private StateMachine<ArticleStage, ArticleActionType> _stateMachine;
		private readonly Dictionary<ArticleActionType,
				StateMachine<ArticleStage, ArticleActionType>.TriggerWithParameters<ArticleStage>> _triggers = new();

		public ArticleStateMachine(ArticleStage articleStage, ProductionDbContext _dbContext)
    {
				 _stateMachine = new(articleStage);
				var transitions = _dbContext.GetCached<ArticleStageTransition>();

				foreach (var transition in transitions)
				{
						var trigger = _stateMachine.SetTriggerParameters<ArticleStage>(transition.ActionType);
						_triggers[transition.ActionType] = trigger;
						_stateMachine.Configure(transition.CurrentStage)
								.PermitDynamic(trigger, articleStage => transition.DestinationStage);
				}
		}

		public bool CanFire(ArticleStage articleStage, ArticleActionType actionType)
		{
				var trigger = _triggers[actionType];
				return _stateMachine.CanFire(trigger, articleStage);
		}
}
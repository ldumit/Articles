using Articles.Abstractions;
using Submission.Domain.Entities;
using Submission.Domain.Enums;
using Submission.Persistence;
using Stateless;

namespace Submission.Application.StateMachines;

public delegate ArticleStateMachine ArticleStateMachineFactory(ArticleStage articleStage);

public class ArticleStateMachine
{
		private StateMachine<ArticleStage, ArticleActionType> _stateMachine;

		public ArticleStateMachine(ArticleStage articleStage, SubmissionDbContext _dbContext)
    {
				 _stateMachine = new(articleStage);
				var transitions = _dbContext.GetCached<ArticleStageTransition>();

				foreach (var transition in transitions)
				{
						_stateMachine.Configure(transition.CurrentStage)
								.Permit(transition.ActionType, transition.DestinationStage);
				}
		}

		public bool CanFire(ArticleActionType actionType)
		{
				return _stateMachine.CanFire(actionType);
		}
}
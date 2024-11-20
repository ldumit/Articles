using Stateless;
using Blocks.Core.Cache;
using Articles.Abstractions.Enums;
using Submission.Domain.Entities;
using Submission.Domain.Enums;
using Submission.Persistence;
using Submission.Domain.StateMachines;
using Microsoft.Extensions.Caching.Memory;

namespace Submission.Application.StateMachines;

//todo - reimplement it directly with the database (cached), remove stateless library
public class ArticleStateMachine : IArticleStateMachine
{
		private StateMachine<ArticleStage, ArticleActionType> _stateMachine;
		public ArticleStateMachine(ArticleStage articleStage, IMemoryCache cache)
		{
				_stateMachine = new(articleStage);
				var transitions = cache.Get<List<ArticleStageTransition>>();

				foreach (var transition in transitions)
				{
						_stateMachine.Configure(transition.CurrentStage)
								.Permit(transition.ActionType, transition.DestinationStage);
				}
		}


		//public ArticleStateMachine(ArticleStage articleStage, SubmissionDbContext dbContext)
  //  {
		//		 _stateMachine = new(articleStage);
		//		var transitions = dbContext.GetAllCached<ArticleStageTransition>();

		//		foreach (var transition in transitions)
		//		{
		//				_stateMachine.Configure(transition.CurrentStage)
		//						.Permit(transition.ActionType, transition.DestinationStage);
		//		}
		//}

		public bool CanFire(ArticleActionType actionType)
		{
				return _stateMachine.CanFire(actionType);
		}
}
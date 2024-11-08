using Articles.Abstractions;
using Articles.Exceptions;
using Submission.Domain.Enums;
using Submission.Domain.StateMachines;

namespace Submission.Application.StateMachines;

//public static class ArticleStateMachineFactoryExtensions
//{
//		public static void CheckStageTransitionOrThrow(this ArticleStateMachineFactory factory, ArticleStage articleStage, ArticleActionType actionType)
//		{
//				var stateMachine = factory(articleStage);

//				if (!stateMachine.CanFire(actionType))
//						throw new BadRequestException($"Action {actionType} not allowed in the {articleStage} article stage");
//		}
//}
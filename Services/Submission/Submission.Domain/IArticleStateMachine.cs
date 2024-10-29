using Articles.Abstractions;
using Articles.Exceptions.Domain;
using Submission.Domain.Enums;

namespace Submission.Domain.StateMachines;

public delegate IArticleStateMachine ArticleStateMachineFactory(ArticleStage articleStage);
public interface IArticleStateMachine
{
		bool CanFire(ArticleActionType actionType);
}

public static class Extensions
{
		public static void ValidateStageTransition(this ArticleStateMachineFactory factory, ArticleStage articleStage, ArticleActionType actionType)
		{
				var stateMachine = factory(articleStage);

				if (!stateMachine.CanFire(actionType))
						throw new DomainException($"Action {actionType} not allowed in the {articleStage} article stage");
		}
}

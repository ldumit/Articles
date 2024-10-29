using Articles.EntityFrameworkCore;
using FluentValidation;
using Production.API.Features.Shared;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.AssignTypesetter;

public record AssignTypesetterCommand : ArticleCommand
{
		public int TypesetterId { get; init; }
		public override ArticleActionType ActionType => ArticleActionType.AssignTypesetter;
}

public class AssignTypesetterCommandValidator : ArticleCommandValidator<AssignTypesetterCommand>
{
		public AssignTypesetterCommandValidator()
		{
				RuleFor(r => r.ArticleId).GreaterThan(0);
				RuleFor(r => r.TypesetterId).GreaterThan(0);

				RuleFor(r => r).MustAsync(async (r, _, cancellation) => await IsActionValid(r))
						.WithMessage("Action not allowed");
		}

		protected virtual async Task<bool> IsActionValid(ArticleCommand action)
		{
				var articleRepository = Resolve<ArticleRepository>();
				var article = await articleRepository.GetByIdOrThrowAsync(action.ArticleId);
				
				var stateMachineFactory = Resolve<ArticleStateMachineFactory>();
				var stateMachine = stateMachineFactory(article.Stage);

				return stateMachine.CanFire(action.ActionType);
		}
}
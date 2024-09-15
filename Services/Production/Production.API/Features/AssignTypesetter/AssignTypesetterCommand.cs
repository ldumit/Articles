using FluentValidation;
using Production.API.Features.Shared;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.AssignTypesetter;

public record AssignTypesetterCommand : ArticleCommand<ArticleCommandResponse>
{
		public int TypesetterId { get; init; }
		public override ArticleActionType ActionType => ArticleActionType.AssignTypesetter;
}

//todo - validate 
public class AssignTypesetterCommandValidator : ArticleCommandValidator<AssignTypesetterCommand>
{
		public AssignTypesetterCommandValidator()
		{
				RuleFor(r => r.ArticleId).GreaterThan(0);
				RuleFor(r => r.TypesetterId).GreaterThan(0);

				//todo - validate action agaisnt the database
		}
}
using Articles.Abstractions;
using Production.Domain.Enums;

namespace Production.API.Features.AssignTypesetter;

public record AssignTypesetterCommand : ArticleCommand2<ArticleCommandResponse>
{
		public int TypesetterId { get; init; }
		public override ActionType ActionType => ActionType.AssignTypesetter;

}
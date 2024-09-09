using Production.Domain.Enums;

namespace Production.API.Features;

public record RequestNewFileCommand : FileActionWithBodyCommand
{
		protected override ActionType GetActionType() => ActionType.RequestNew;
}

public record RequestNewAuthorProofCommand : RequestNewFileCommand
{
}

public record RequestNewFinalFileCommand : RequestNewFileCommand
{
}
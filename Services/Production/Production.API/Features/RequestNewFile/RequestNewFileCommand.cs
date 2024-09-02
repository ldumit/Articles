using Production.Domain.Enums;

namespace Production.API.Features;

public record RequestNewFileCommand : FileActionWithBodyCommand
{
		internal override ActionType ActionType => ActionType.RequestNew;
}

public record RequestNewAuthorProofCommand : RequestNewFileCommand
{
}

public record RequestNewFinalFileCommand : RequestNewFileCommand
{
}
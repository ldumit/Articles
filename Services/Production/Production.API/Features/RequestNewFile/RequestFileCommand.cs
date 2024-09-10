using Production.Domain.Enums;

namespace Production.API.Features;

public record RequestFileCommand : FileActionWithBodyCommand
{
		protected override ActionType GetActionType() => ActionType.RequestNew;
}

//todo do I need authorsProof? it might be confusing, the final file should be enough
public record RequestAuthorProofCommand : RequestFileCommand
{
}

public record RequestFinalFileCommand : RequestFileCommand
{
}

public record RequestAuthorFileCommand : RequestFileCommand
{
    public int AssetNumber { get; set; }
}
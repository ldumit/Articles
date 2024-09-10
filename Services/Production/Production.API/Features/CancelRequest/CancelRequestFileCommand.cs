using Production.Domain.Enums;

namespace Production.API.Features;


//todo do I need authorsProof? it might be confusing, the final file should be enough
public record CancelRequestAuthorProofCommand : RequestFileCommand
{
}

public record CancelRequestFinalFileCommand : RequestFileCommand
{
}

public record CancelRequestAuthorFileCommand : RequestFileCommand
{
    public int AssetNumber { get; init; }
}
using Production.API.Features.RequestFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.Cancel;

public record CancelRequestFinalAssetsCommand : RequestMultipleAssetsCommand
{
		public override AssetActionType ActionType => AssetActionType.CancelRequest;
}

public record CancelRequestAuthorFilesCommand : RequestMultipleAssetsCommand
{
}
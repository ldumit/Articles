using Production.API.Features.RequestFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.Cancel;

public record CancelRequestFinalFilesCommand : RequestMultipleFilesCommand
{
		public override AssetActionType ActionType => AssetActionType.CancelRequest;
}

public record CancelRequestAuthorFilesCommand : RequestMultipleFilesCommand
{
}
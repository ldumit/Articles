using Submission.API.Features.RequestFiles.Shared;
using Submission.Domain.Enums;

namespace Submission.API.Features.RequestFiles.Cancel;

public record CancelRequestFinalAssetsCommand : RequestMultipleAssetsCommand
{
		public override AssetActionType ActionType => AssetActionType.CancelRequest;
}

public record CancelRequestAuthorFilesCommand : RequestMultipleAssetsCommand
{
}
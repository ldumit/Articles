using Production.API.Features.RequestFiles.Shared;

namespace Production.API.Features.RequestFiles.Cancel;

public record CancelRequestFinalFilesCommand : RequestMultipleFilesCommand
{
}

public record CancelRequestAuthorFilesCommand : RequestMultipleFilesCommand
{
}
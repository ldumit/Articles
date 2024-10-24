using Articles.Security;
using FastEndpoints;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Authorization;
using Submission.Persistence.Repositories;

namespace Submission.API.Features.DownloadFile;

[Authorize(Roles = $"{Role.POF},{Role.CORAUT},{Role.TSOF}")]
[HttpGet("articles/{articleId:int}/files/{fileId:int}")]
public class DownloadFileEndpoint(FileRepository _fileRepository, IFileService _fileService) : Endpoint<DownloadFileCommand>
{

    public override async Task HandleAsync(DownloadFileCommand command, CancellationToken ct)
    {
        var file = await _fileRepository.GetByIdAsync(command.ArticleId, command.FileId);

        var (fileStream, contentType) = await _fileService.DownloadFileAsync(file.FileServerId);

        await SendStreamAsync(fileStream, file.Name.Value, file.Size, contentType);

        //await SendNotFoundAsync(ct); // handle file not found
    }
}
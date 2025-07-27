using FileStorage.Contracts;
using Production.Persistence.Repositories;

namespace Production.API.Features.DownloadFile;

[Authorize(Roles = $"{Role.POF},{Role.CORAUT},{Role.TSOF}")]
[HttpGet("articles/{articleId:int}/files/{fileId:int}")]
public class DownloadFileEndpoint(FileRepository _fileRepository, IFileService _fileService) : Endpoint<DownloadFileCommand>
{
		public override async Task HandleAsync(DownloadFileCommand command, CancellationToken ct)
		{
				var file = await _fileRepository.GetByIdAsync(command.ArticleId, command.FileId);

				var (fileStream, fileMetada) = await _fileService.DownloadAsync(file.FileServerId, ct);

				await SendStreamAsync(fileStream, file.Name.Value, file.Size, fileMetada.ContentType);				
		}
}
using FileStorage.Contracts;
using MediatR;
using Submission.Persistence.Repositories;

namespace Submission.Application.Features.DownloadFile;

public class DownloadFileEndpoint(FileRepository _fileRepository, IFileService _fileService) : IRequestHandler<DownloadFileCommand, Unit>
{

		public async Task<Unit> Handle(DownloadFileCommand command, CancellationToken cancellationToken)
		{
				var file = await _fileRepository.GetByIdAsync(command.ArticleId, command.FileId);

				var (fileStream, contentType) = await _fileService.DownloadFileAsync(file.FileServerId);

				//await SendStreamAsync(fileStream, file.Name.Value, file.Size, contentType);

				//await SendNotFoundAsync(ct); // handle file not found
				return Unit.Value;
		}
}
using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

public interface IFileService
{
		Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, Dictionary<string, string>? tags = null);
		//Task<UploadResponse> UploadFile(string fileName, IFormFile file);
		//Task<UploadResponse> UploadFile(UploadRequest request);

		Task<bool> TryDeleteFileAsync(string filePath);

		Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string filePath);
}

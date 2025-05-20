using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

public interface IFileService
{
		Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null);

		Task<IEnumerable<string>> FindFileIdsByTagAsync(string key, string value);

		Task<bool> TryDeleteFileAsync(string fileId);
		Task<bool> TryDeleteFileByTagAsync(string key, string value);

		Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId);
		Task<(Stream FileStream, string ContentType)> DownloadFileByTagAsync(string key, string value);
}

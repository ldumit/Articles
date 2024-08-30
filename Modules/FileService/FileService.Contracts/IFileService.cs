using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

public interface IFileService
{
		Task<UploadResponse> UploadFile(string containerName, string fileName, IFormFile file, string[] tags);
		Task<UploadResponse> UploadFile(string fileName, IFormFile file);
		Task<UploadResponse> UploadFile(UploadRequest request);
}

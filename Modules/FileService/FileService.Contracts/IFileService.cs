using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

public interface IFileService
{
		Task<UploadResponse> UploadFile(string filePath, IFormFile file, Dictionary<string, string>? tags = null);
		//Task<UploadResponse> UploadFile(string fileName, IFormFile file);
		//Task<UploadResponse> UploadFile(UploadRequest request);
}

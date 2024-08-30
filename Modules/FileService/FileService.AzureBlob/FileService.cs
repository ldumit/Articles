using Azure.Core;
using Azure.Storage.Blobs;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FileStorage.AzureBlob;

public class FileService(BlobServiceClient _blobServiceClient, IOptions<FileServerOptions> _fileServerOptions) : IFileService
{
		public async Task CreateContainer(string name)
		{
				//todo create container at startup
				BlobContainerClient containerClient = await _blobServiceClient.CreateBlobContainerAsync(name);
		}

		public async Task<UploadResponse> UploadFile(string containerName, string fileName, IFormFile file, params string[] tags)
		{
				var container = _blobServiceClient.GetBlobContainerClient(_fileServerOptions.Value.Container);

				var blob = container.GetBlobClient(fileName);
				var result = await blob.UploadAsync(file.OpenReadStream(), overwrite: false);
				
				//talk - use tags/metadata to search the files based on the original entity name/id
				var res = blob.SetTags(new Dictionary<string, string>());

				return new UploadResponse("", "");
		}

		public async Task<UploadResponse> UploadFile(string fileName, IFormFile file)
		{
				return await UploadFile(_fileServerOptions.Value.Container, fileName, file);
		}

		public async Task<UploadResponse> UploadFile(UploadRequest request)
		{
				var fileName = $"{request.EntityId}/{request.DocumentName}/{request.Version}";
				return await UploadFile(_fileServerOptions.Value.Container, fileName, request.File);
		}
}

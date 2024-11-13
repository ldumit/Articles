using Blocks.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FileStorage.AzureBlob;

public class FileService : IFileService
{
		private readonly BlobServiceClient _blobServiceClient;
		private readonly FileServerOptions _fileServerOptions;
		public FileService(BlobServiceClient blobServiceClient, IOptions<FileServerOptions> fileServerOptions)
				=> (_blobServiceClient, _fileServerOptions) = (blobServiceClient, fileServerOptions.Value);						

    public async Task CreateContainerAsync(string name)
		{
				//todo create container at startup
				BlobContainerClient containerClient = await _blobServiceClient.CreateBlobContainerAsync(name);
		}

		public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null)
		{
				var blob = await GetBlob(filePath);
				var result = await blob.UploadAsync(file.OpenReadStream(), overwrite: overwrite);

				//talk - use tags/metadata to search the files based on the original entity name/id
				if (!tags.IsNullOrEmpty())
						blob.SetTags(tags);

				return new UploadResponse(filePath, file.FileName, file.Length, result.Value.VersionId);
		}

		public async Task<bool> TryDeleteFileAsync(string filePath)
		{
				if(filePath.IsNullOrEmpty())
						return false;		

				try
				{
						var blob = await GetBlob(filePath);
						return await blob.DeleteIfExistsAsync();
				}
				catch (Exception) { return false; }
		}
		public async Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string filePath)
		{
				var blob = await GetBlob(filePath);

				if (!await blob.ExistsAsync())
						throw new FileNotFoundException($"File '{filePath}' not found in container '{_fileServerOptions.Container}'.");

				BlobDownloadInfo download = await blob.DownloadAsync();
				return (download.Content, download.ContentType);				
		}

		private async Task<BlobClient> GetBlob(string filePath)
		{
				var container = _blobServiceClient.GetBlobContainerClient(_fileServerOptions.Container);
				if(!await container.ExistsAsync())
						await CreateContainerAsync(_fileServerOptions.Container);
				return container.GetBlobClient(filePath);
		}

		//public async Task<UploadResponse> UploadFile(string fileName, IFormFile file)
		//{
		//		return await UploadFile(_fileServerOptions.Value.Container, fileName, file);
		//}

		//public async Task<UploadResponse> UploadFile(UploadRequest request)
		//{			
		//		return await UploadFile(_fileServerOptions.Value.Container, request.FilePath, request.File, request.Tags);
		//}
}

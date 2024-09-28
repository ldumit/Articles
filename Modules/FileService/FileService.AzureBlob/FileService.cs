using Articles.System;
using Azure.Core;
using Azure.Storage.Blobs;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FileStorage.AzureBlob;

public class FileService(BlobServiceClient _blobServiceClient, IOptions<FileServerOptions> _fileServerOptions) : IFileService
{
		public async Task CreateContainer(string name)
		{
				//todo create container at startup
				BlobContainerClient containerClient = await _blobServiceClient.CreateBlobContainerAsync(name);
		}

		public async Task<UploadResponse> UploadFile(string filePath, IFormFile file, Dictionary<string, string>? tags = null)
		{
				var container = _blobServiceClient.GetBlobContainerClient(_fileServerOptions.Value.Container);

				var blob = container.GetBlobClient(filePath);
				var result = await blob.UploadAsync(file.OpenReadStream(), overwrite: false);
				
				//talk - use tags/metadata to search the files based on the original entity name/id
				if(tags.IsNullOrEmpty())
						blob.SetTags(tags);

				return new UploadResponse(filePath, file.FileName, file.Length, result.Value.VersionId);
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

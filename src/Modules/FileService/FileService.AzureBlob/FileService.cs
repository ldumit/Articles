using Blocks.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FileStorage.AzureBlob;

public class FileService : IFileService
{
		private readonly BlobContainerClient _containerClient;
		private readonly FileServerOptions _fileServerOptions;

		public FileService(BlobContainerClient containerClient, IOptions<FileServerOptions> options)
				=> (_containerClient, _fileServerOptions) = (containerClient, options.Value);

		public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null)
		{
				var blob = _containerClient.GetBlobClient(filePath);
				var result = await blob.UploadAsync(file.OpenReadStream(), overwrite: overwrite);

				//talk - use tags/metadata to search the files based on the original entity name/id
				if (!tags.IsNullOrEmpty())
						blob.SetTags(tags);

				return new UploadResponse(filePath, file.FileName, file.Length, filePath);
		}

		public async Task<bool> TryDeleteFileAsync(string filePath)
		{
				if (filePath.IsNullOrEmpty())
						return false;

				try
				{
						var blob = _containerClient.GetBlobClient(filePath);
						return await blob.DeleteIfExistsAsync();
				}
				catch (Exception) { return false; }
		}

		public async Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string filePath)
		{
				var blob = _containerClient.GetBlobClient(filePath);

				if (!await blob.ExistsAsync())
						throw new FileNotFoundException($"File '{filePath}' not found in container '{_fileServerOptions.Container}'.");

				BlobDownloadInfo download = await blob.DownloadAsync();
				return (download.Content, download.ContentType);
		}

		public Task<bool> TryDeleteFileByTagAsync(string key, string value)
		{
				throw new NotImplementedException();
		}

		public Task<(Stream FileStream, string ContentType)> DownloadFileByTagAsync(string key, string value)
		{
				throw new NotImplementedException();
		}

		public async Task<IEnumerable<string>> FindFileIdsByTagAsync(string key, string value)
		{
				var query = $"@tag.{key} = '{value}'";
				var results = new List<string>();

				await foreach (var blobItem in _containerClient.FindBlobsByTagsAsync(query))
				{
						results.Add(blobItem.BlobName); // this is your fileId
				}

				return results;
		}
}

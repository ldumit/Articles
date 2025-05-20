using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Microsoft.AspNetCore.Http;
using FileStorage.Contracts;
using Microsoft.Extensions.Options;

namespace FileStorage.MongoGridFS;

public class FileService : IFileService
{
		private readonly GridFSBucket _bucket;
		private readonly MongoGridFsFileStorageOptions _options;

		private const string FilePathMetadataKey = "filePath";
		private const string ContentTypeMetadataKey = "contentType";
		private const string DefaultContentType = "application/octet-stream";

		public FileService(GridFSBucket bucket, IOptions<MongoGridFsFileStorageOptions> options)
				=> (_bucket, _options) = (bucket, options.Value);

		public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null)
		{
				if (file.Length > _options.FileSizeLimitInBytes)
						throw new InvalidOperationException($"File exceeds maximum allowed size of {_options.FileSizeLimitInMB} MB.");

				if (overwrite) // Delete existing if overwrite is true
						await TryDeleteFileAsync(filePath);

				// add filePath & ContentType to the tags
				var metadata = new BsonDocument(tags ?? new Dictionary<string, string>())
				{
						{ FilePathMetadataKey, filePath },
						{ ContentTypeMetadataKey, file.ContentType }
				};

				var uploadOptions = new GridFSUploadOptions
				{
						Metadata = metadata,
						ChunkSizeBytes = _options.ChunkSizeBytes
				};

				ObjectId fileId;
				using (var stream = file.OpenReadStream())
				{
						fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, uploadOptions);
				}

				return new UploadResponse(
						FilePath: filePath,
						FileName: file.FileName,
						FileSize: file.Length,
						FileId: fileId.ToString()
				);
		}

		public async Task<IEnumerable<string>> FindFileIdsByTagAsync(string key, string value)
		{
				var filter = Builders<GridFSFileInfo>.Filter.Eq($"metadata.{key}", value);
				var fileInfos = await _bucket.Find(filter).ToListAsync();

				return fileInfos.Select(f => f.Id.ToString());
		}

		public async Task<bool> TryDeleteFileByTagAsync(string key, string value)
		{
				var filter = Builders<GridFSFileInfo>.Filter.Eq($"metadata.{key}", value);
				var fileInfo = await _bucket.Find(filter).FirstOrDefaultAsync();
				if (fileInfo == null)
						return false;

				await _bucket.DeleteAsync(fileInfo.Id);
				return true;
		}

		public async Task<bool> TryDeleteFileAsync(string fileId)
		{
				if (!ObjectId.TryParse(fileId, out var objectId))
						return false;

				try
				{
						await _bucket.DeleteAsync(objectId);
						return true;
				}
				catch (GridFSFileNotFoundException) { return false; }
		}

		public async Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId)
		{
				if (!ObjectId.TryParse(fileId, out var objectId))
						throw new FileNotFoundException($"Invalid file ID: {fileId}");

				return await DownloadByIdAsync(objectId);
		}

		public async Task<(Stream FileStream, string ContentType)> DownloadFileByTagAsync(string key, string value)
		{
				var filter = Builders<GridFSFileInfo>.Filter.Eq($"metadata.{key}", value);
				var fileInfo = await _bucket.Find(filter).FirstOrDefaultAsync();

				if (fileInfo == null)
						throw new FileNotFoundException($"No file found with tag [{key}={value}]");

				return await DownloadByIdAsync(fileInfo.Id);
		}

		private async Task<(Stream FileStream, string ContentType)> DownloadByIdAsync(ObjectId fileId)
		{
				var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", fileId)).FirstOrDefaultAsync();
				if (fileInfo == null)
						throw new FileNotFoundException($"No file found with ID: {fileId}");

				var stream = await _bucket.OpenDownloadStreamAsync(fileId);
				var contentType = fileInfo.Metadata?.GetValue(ContentTypeMetadataKey, DefaultContentType)?.AsString ?? DefaultContentType;

				return (stream, contentType);
		}
}


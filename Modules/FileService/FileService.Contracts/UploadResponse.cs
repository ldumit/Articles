using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

public record FileMetadata(string EntityNamme, string EntityId);

public record UploadRequest(string FilePath, IFormFile File, Dictionary<string,string>? Tags = null);

public record UploadRequest2(string EntityId, string DocumentName, int Version, IFormFile File);
public record UploadResponse(string FilePath, string FileId);

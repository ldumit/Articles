using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

public record UploadRequest(string EntityId, string DocumentName, int Version, IFormFile File);
public record UploadResponse(string EntityId, string DocumentId);

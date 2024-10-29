namespace Submission.Application.Dtos;

public record FileDto(
		string Name,
		string OriginalName,
		long Size,
		string FileServerId);
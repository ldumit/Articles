using Submission.Domain.Enums;

namespace Submission.Application.Dtos;

public record AssetDto(
		int Id, 
		string Name, 
		byte Number, 
		AssetState State,
		AssetType Type,
		AssetCategory CategoryId,
		IReadOnlyList<FileDto> Files);
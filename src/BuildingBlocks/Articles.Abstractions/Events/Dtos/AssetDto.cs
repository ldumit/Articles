using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public record class AssetDto(
		int Id,
		string Name,
		AssetCategory CategoryId,
		AssetType Type,
		FileDto File
		);

public record class FileDto(
		string OriginalName,
		string Name,
		string Extension,
		string FileServerId,
		long Size
		);

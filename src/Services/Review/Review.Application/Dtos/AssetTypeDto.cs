using Review.Domain.Assets.ValueObjects;

namespace Review.Application.Dtos;

// todo - do I need this DTO?
public record AssetTypeDto
{
		public AssetType Id { get; init; }
		public string Description { get; init; } = null!;
		public FileExtensions AllowedFileExtensions { get; init; } = null!;
		public string DefaultFileExtension { get; init; } = default!;
		public byte MaxNumber { get; init; }
		public byte MaxFileSizeInMB { get; init; }

		public bool AllowsMultipleAssets => MaxNumber > 0;
}

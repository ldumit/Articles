using Submission.Domain.Enums;
using Submission.Domain.ValueObjects;

namespace Submission.Application.Dtos;

public class AssetTypeDto
{
		public AssetType Id { get; init; }
		public AssetType Name { get; init; }
		public string Description { get; init; } = null!;
		public AssetCategory DefaultCategoryId { get; init; }
		public AllowedFileExtensions AllowedFileExtensions { get; init; } = null!;
		public string DefaultFileExtension { get; init; } = default!;
		public byte MaxNumber { get; init; }
		public byte MaxFileSizeInMB { get; init; }

		public bool AllowsMultipleAssets => MaxNumber > 0;
}

using Production.Domain.Entities;

namespace Production.Domain.ValueObjects;

internal class AssetName
{
		public string Value { get; init; } = default!;

		public static AssetName From(AssetType assetType)
		{
				return new AssetName { Value = assetType.ToString() };
		}
}

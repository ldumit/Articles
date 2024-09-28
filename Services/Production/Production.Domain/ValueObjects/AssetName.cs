using Articles.Entitities;
using Newtonsoft.Json;
using Production.Domain.Entities;

namespace Production.Domain.ValueObjects;

public record AssetName: ValueObject<string>
{
		[JsonConstructor]
		private AssetName(string value) => Value = value;

		public static AssetName FromAssetType(AssetType assetType)
				=> new AssetName(assetType.ToString());
}

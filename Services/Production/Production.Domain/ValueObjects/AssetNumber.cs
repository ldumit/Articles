using Articles.Entitities;
using Newtonsoft.Json;
using Production.Domain.Entities;

namespace Production.Domain.ValueObjects;

public record AssetNumber : ValueObject<byte>
{
		[JsonConstructor]
		private AssetNumber(byte value) => Value = value;

		public static AssetNumber FromNumber(byte number,  AssetType assetType)
		{
				ArgumentOutOfRangeException.ThrowIfGreaterThan(number, assetType.MaxNumber);
				return new AssetNumber(number);
		}

		// added for Link queries
		public static implicit operator byte(AssetNumber assetNumber) => assetNumber.Value;
}

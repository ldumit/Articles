using Newtonsoft.Json;

namespace Review.Domain.ValueObjects;

//talk - explain why class and not record. the record provides its own set of ToString, GethashCode implementations and it doesn't work with inheritence
public class AssetNumber : SingleValueObject<byte>
{
		[JsonConstructor]
		private AssetNumber(byte value) => Value = value;

		public static AssetNumber FromNumber(byte number,  AssetTypeDefinition assetType)
		{
				ArgumentOutOfRangeException.ThrowIfGreaterThan(number, assetType.MaxAssetCount, "Asset type max number already reached");
				return new AssetNumber(number);
		}

		// added for Link queries
		public static implicit operator byte(AssetNumber assetNumber) => assetNumber.Value;
}

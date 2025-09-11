﻿using Newtonsoft.Json;
using System.Xml.Linq;

namespace Review.Domain.Assets.ValueObjects;

//talk - explain why class and not record. the record provides its own set of ToString, GethashCode implementations and it doesn't work with inheritence
public class AssetNumber : SingleValueObject<int>
{
		[JsonConstructor]
		private AssetNumber(int value) => Value = value;

		public static AssetNumber FrmSubmission(int number)
		{
				ArgumentOutOfRangeException.ThrowIfLessThan(number, 0, "Asset Number must pe positive");
				return new AssetNumber(number);
		}

		public static AssetNumber Create(AssetTypeDefinition assetType, int assetCount)
		{
				int number = assetType.AllowsMultipleAssets ? assetCount + 1 : 0;
				ArgumentOutOfRangeException.ThrowIfGreaterThan(number, assetType.MaxAssetCount, "Asset type max number already reached"); //todo - create a Guard
				return new AssetNumber(number);
		}

		// added for Link queries
		public static implicit operator int(AssetNumber assetNumber) => assetNumber.Value;
}

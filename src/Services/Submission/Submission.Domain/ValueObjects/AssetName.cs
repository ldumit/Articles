﻿using Newtonsoft.Json;

namespace Submission.Domain.ValueObjects;

public class AssetName: StringValueObject
{
		[JsonConstructor]
		private AssetName(string value) => Value = value;

		public static AssetName FromAssetType(AssetTypeDefinition assetType)
				=> new AssetName(assetType.Name.ToString());
}

using Blocks.Entitities;
using Newtonsoft.Json;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

public class FileName: StringValueObject
{
		[JsonConstructor]
		private FileName(string value) => Value = value;

		public static FileName From(Asset asset, FileExtension extension)
		{
				var assetName = asset.Name.Value.Replace("'", "").Replace(" ", "-");
				var assetNumber = asset.Number > 0 ? asset.Number.ToString() : string.Empty;
				return new FileName($"{assetName}{assetNumber}.{extension}");
		}
}

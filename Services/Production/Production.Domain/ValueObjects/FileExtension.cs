using Articles.Entitities;
using Newtonsoft.Json;
using Production.Domain.Entities;

namespace Production.Domain.ValueObjects;

public class FileExtension: StringValueObject
{
		[JsonConstructor]
		private FileExtension(string value) => Value = value;

    public static FileExtension FromAssetType(AssetType assetType)
    {
        return new FileExtension(assetType.DefaultFileExtension);
    }

    public static FileExtension FromFileName(string fileName, AssetType assetType)
    {
        var extension = Path.GetExtension(fileName).Remove(0, 1); //removing the '.'
				ArgumentException.ThrowIfNullOrWhiteSpace(extension);
				ArgumentOutOfRangeException.ThrowIfNotEqual(
            assetType.AllowedFileExtensions.IsValidExtension(extension), true); 

				return new FileExtension(extension);
    }
}

using Production.Domain.Entities;

namespace Production.Domain.ValueObjects;


//public record OrderName
//{
//		private const int DefaultLength = 5;
//		public string Value { get; }
//		private OrderName(string value) => Value = value;
//		public static OrderName Of(string value)
//		{
//				ArgumentException.ThrowIfNullOrWhiteSpace(value);
//				//ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

//				return new OrderName(value);
//		}
//}

public record FileExtension
{
		public string Value { get; set; }
    public FileExtension(string value) => Value = value;
		public static FileExtension FromAssetType(AssetType assetType)
		{
				return new FileExtension(assetType.DefaultFileExtension);
		}

		public static  FileExtension FromFileName(string fileName, AssetType assetType)
    {
				var extension = Path.GetExtension(fileName);
        assetType.AllowedFileExtensions.IsValidExtension(extension);

				return new FileExtension(extension);
    }
}

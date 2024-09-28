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
    public string Value { get; init; } = string.Empty;
    private FileExtension(string value) => Value = value;
    public static FileExtension FromAssetType(string assetType)
    {
        return new FileExtension(assetType);
    }
}

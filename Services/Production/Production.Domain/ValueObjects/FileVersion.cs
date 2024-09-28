using Articles.Entitities;
using Newtonsoft.Json;
using Production.Domain.Entities;

namespace Production.Domain.ValueObjects;

public record FileVersion: ValueObject<byte>, IEquatable<byte>
{
		[JsonConstructor]
		private FileVersion(byte value) => Value = value;

		public static FileVersion FromAsset(Asset asset)
		{
				ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(asset.CurrentFile!.Version.Value, byte.MaxValue);

				return new FileVersion((byte)(asset.CurrentFile!.Version.Value + 1));
		}
}

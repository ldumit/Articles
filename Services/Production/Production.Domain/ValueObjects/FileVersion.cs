using Articles.Entitities;
using Newtonsoft.Json;
using Production.Domain.Entities;

namespace Production.Domain.ValueObjects;

public class FileVersion: ValueObject<byte>
{
		[JsonConstructor]
		private FileVersion(byte value) => Value = value;

		public static FileVersion FromAsset(Asset asset)
		{
				ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(asset.CurrentVersion, byte.MaxValue);

				return new FileVersion(asset.CalculateNextVersion());
		}

		public static implicit operator byte(FileVersion version) => version.Value;
}

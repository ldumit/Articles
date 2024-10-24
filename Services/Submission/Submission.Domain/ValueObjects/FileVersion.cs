using Articles.Entitities;
using Newtonsoft.Json;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

public class FileVersion: SingleValueObject<byte>
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

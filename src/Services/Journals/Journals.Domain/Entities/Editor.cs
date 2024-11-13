using Blocks.Redis;
using Redis.OM.Modeling;

namespace Journals.Domain.Entities;

[Document(StorageType = StorageType.Hash)]
public class Editor : Entity
{
		[Indexed]
    public required string FullName { get; init; }
		[Indexed]
		public required string Location { get; init; }
		[Indexed]
		public required string University { get; init; }
}

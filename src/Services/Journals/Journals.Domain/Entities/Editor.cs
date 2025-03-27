using Blocks.Redis;
using Redis.OM.Modeling;

namespace Journals.Domain.Entities;

[Document(StorageType = StorageType.Hash)]
public class Editor : Entity
{
		// Editor.Id = Editor.UserId. Editors are Users therefore they don't need their own IDs

		[Indexed]
    public required string FullName { get; init; }
		[Indexed]
		public required string Location { get; init; }
		[Indexed]
		public required string University { get; init; }
}

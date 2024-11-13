using Blocks.Redis;
using Redis.OM.Modeling;

namespace Journals.Domain.Entities;

[Document(StorageType = StorageType.Json)]
public class Section : Entity
{
		[Indexed]
		public string Name { get; set; }
		public string Description { get; set; }
    public List<SectionEditor> EditorRoles { get; set; } = new();
    public int ArticlesCount { get; set; }
}

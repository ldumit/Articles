using Blocks.Redis;
using Redis.OM.Modeling;

namespace Journals.Domain.Entities;

[Document(StorageType = StorageType.Json)]
public partial class Journal : Entity
{
		[Indexed]
		public string Abbreviation { get; init; }

		private string _name;
		[Indexed]
		public string Name
		{
				get => _name;
				init
				{
						_name = value;
						NormalizedName = _name.ToLowerInvariant(); // Normalize on set
				}
		}
		[Indexed] 
		//talk about normalizing the Name so we can do case-insensitive searches
		public string NormalizedName { get; init; }

		public string Description { get; init; }
    public string ISSN { get; init; } //unique ID in the publishing world
    public int ChiefEditorId { get; init; }

		//public int DefaultTypesetter { get; set; }

		[Indexed(JsonPath = "$.Name")]
		public List<Section> Sections { get; init; } = new();

		public IReadOnlyCollection<SectionEditor> Editors =>
				Sections.SelectMany(s => s.EditorRoles.Where(e => e.EditorRole == EditorRole.ReviewEditor))
								.Distinct()
								.ToList()
								.AsReadOnly();

		public int ArticlesCount { get; set; }
}


using Redis.OM.Modeling;

namespace Journals.Domain.Entities;

[Document(StorageType = StorageType.Json)]
public partial class Journal : Entity
{
		[Indexed]
		public string Abbreviation { get; set; }
		[Indexed]
		public string Name { get; set; }
		public string Description { get; set; }
    public string ISSN { get; set; } //unique ID in the publishing world

    public int ChiefEditorId { get; set; }

		//public int DefaultTypesetter { get; set; }
		//public List<int> SectionIds { get; set; } = new();

		[Indexed(JsonPath = "$.Name")]
		public List<Section> Sections { get; set; } = new();
}


﻿using Blocks.Redis;
using Journals.Domain.Journals.Enums;
using Journals.Domain.Journals.ValueObjects;
using Redis.OM.Modeling;

namespace Journals.Domain.Journals;

[Document(StorageType = StorageType.Json)]
public partial class Journal : Entity
{
		[Indexed]
		public required string Abbreviation { get; init; }

		private string _name = null!;
		[Searchable]
		public required string Name
		{
				get => _name;
				init
				{
						_name = value;
						NormalizedName = _name.ToLowerInvariant(); // Normalize on set
				}
		}
		[Indexed(Sortable = true)]
		//talk about normalizing the Name so we can do case-insensitive searches
		public required string NormalizedName { get; init; }

		[Searchable]
		public required string Description { get; init; }
    public required string ISSN { get; init; } //unique ID in the publishing world
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


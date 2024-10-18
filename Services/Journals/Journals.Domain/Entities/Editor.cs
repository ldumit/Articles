using Redis.OM.Modeling;

namespace Journals.Domain.Entities;

public enum EditorRole
{
		ChiefEditor,
		AssociateEditor,
		ReviewEditor,
		
}

[Document]
public class Editor : Entity
{
		[Indexed]
    public string FullName { get; set; }
		[Indexed]
		public string Location { get; set; }
		[Indexed]
		public string University { get; set; }
}

using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class FileStatus: EnumEntity<ArticleFileStatusCode>
{

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual FileStatusCode CodeNavigation { get; set; } = null!;

    public virtual ICollection<File> Files { get; } = new List<File>();
}

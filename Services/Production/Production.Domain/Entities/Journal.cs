using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class Journal : TenantEntity
{
    public string? Name { get; set; }

    public string? Abbreviation { get; set; }

    public string? ShortName { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public int DefaultTypesetter { get; set; }

    public virtual ICollection<Article> Articles { get; } = new List<Article>();
}

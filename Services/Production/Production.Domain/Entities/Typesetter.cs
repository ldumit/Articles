using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class Typesetter : TenantEntity
{
    public int UserId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public bool? IsDefault { get; set; }

    public virtual User User { get; set; } = null!;
}

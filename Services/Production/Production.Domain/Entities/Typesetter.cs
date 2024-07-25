using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class Typesetter : Entity
{
    public bool? IsDefault { get; set; }
    public string? CompanyName { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}

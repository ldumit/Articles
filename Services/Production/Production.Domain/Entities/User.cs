using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class User : Entity
{
    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
}

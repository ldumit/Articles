using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class User : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }    
    public string? Title { get; set; }
    public required string Email{ get; set; }
}

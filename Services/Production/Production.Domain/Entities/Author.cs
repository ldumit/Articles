using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Author : Entity
{
    public int? UserId { get; set; }
    public User? User { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Country { get; set; }

    public UserRole Role { get; set; }

    public int ArticleId { get; set; }
    public virtual Article Article { get; set; } = null!;
}

using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class Author : TenantEntity
{
    public int? UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int ArticleId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public int RoleId { get; set; }

    public string RoleCode { get; set; } = null!;

    public string? FullName { get; set; }

    public virtual Article Article { get; set; } = null!;
    public string? Email { get; set; }
    public string? Country { get; set; }

}

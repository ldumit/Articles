using Articles.Entitities;

namespace Auth.Domain.Models;

public class RefreshToken : Entity
{
    public int UserId { get; set; }
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime Created { get; set; }
    public required string CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; }
    public string? RevokedByIp { get; set; } = null!;
    //todo do I need this field?
    public string? ReplacedByToken { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}

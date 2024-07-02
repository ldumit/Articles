namespace Articles.AspNetCore;

public interface IUserClaimsProvider
{
    public string UserRole { get; }
}

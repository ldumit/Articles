using Microsoft.AspNetCore.Identity;

namespace Auth.Domain;

public class User : IdentityUser<int>
{
		public DateTime RegistrationDate { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool IsDeleted { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = new();

    public string FirstName { get; set; }
    public string LastName { get; set; }
		public string Position { get; set; }
		public string PictureUrl { get; set; }
		public string CompanyName { get; set; }

    public int? EmployeeId { get; set; }
}

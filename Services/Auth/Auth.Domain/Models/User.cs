using Articles.Entitities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

public class User : IdentityUser<int>, IAggregateEntity<int>, IEntity<int>
{
		public required string FirstName { get; set; }
    public required string LastName { get; set; }
		
		public string FullName => FirstName + " " + LastName;
		public required Gender Gender { get; set; }
		
		public string? Position { get; set; } = null!;
		public string? PictureUrl { get; set; } = null!;
		public string? CompanyName { get; set; } = null!;

		public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
		public DateTime? LastLogin { get; set; }


		public List<RefreshToken> RefreshTokens { get; set; } = new();
		public virtual List<UserRole> UserRoles { get; set; } = new();

		//audited
		public int CreatedById { get; set; } = default!;
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public int? LastModifiedById { get; set; } = 0;
		public DateTime? LasModifiedOn { get; set; } = DateTime.UtcNow;
}

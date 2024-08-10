using Articles.Entitities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain;

public class User : IdentityUser<int>, IAuditedEntity<int>, IEntity<int>
{
		public string FirstName { get; set; }
    public string LastName { get; set; }
		public string Position { get; set; }
		public string PictureUrl { get; set; }
		public string CompanyName { get; set; }

		public DateTime RegistrationDate { get; set; }
		public DateTime? LastLogin { get; set; }


		public List<RefreshToken> RefreshTokens { get; set; } = new();
		public virtual List<UserRole> UserRoles { get; set; } = new();

		//audited
		public int CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
		public int? LastModifiedById { get; set; }
		public DateTime? LasModifiedOn { get; set; }
}

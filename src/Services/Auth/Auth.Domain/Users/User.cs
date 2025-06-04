using Auth.Domain.Users.Enums;
using Auth.Domain.Users.ValueObjects;
using Blocks.Domain;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Users;

public partial class User : IdentityUser<int>, IAggregateEntity<int>
{
		public required string FirstName { get; set; }
    public required string LastName { get; set; }		
		public string FullName => FirstName + " " + LastName;

		public required Gender Gender { get; set; }
    public HonorificTitle? Honorific{ get; set; }

		public ProfessionalProfile? ProfessionalProfile { get; set; }


		public string? PictureUrl { get; set; } = null!;
		public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
		public DateTime? LastLogin { get; set; }

		private List<UserRole> _userRoles = new List<UserRole>();
		public virtual IReadOnlyList<UserRole> UserRoles => _userRoles;

		public List<RefreshToken> RefreshTokens { get; set; } = new();

		//Audit
		public int CreatedById { get; set; } = default!;
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public int? LastModifiedById { get; set; } = 0;
		public DateTime? LasModifiedOn { get; set; } = DateTime.UtcNow;

		//Aggregate
		private List<IDomainEvent> _domainEvents = new();
		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
		public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
		public void ClearDomainEvents() => _domainEvents.Clear();
}

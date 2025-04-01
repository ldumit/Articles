using Blocks.Domain;
using Blocks.Entitities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

public partial class User : IdentityUser<int>, IAggregateEntity<int>
{
		public required string FirstName { get; set; }
    public required string LastName { get; set; }
		
		public string FullName => FirstName + " " + LastName;
		public required Gender Gender { get; set; }

		public string? Title { get; set; } = null!;
    public string? Position { get; set; } = null!;
		public string? PictureUrl { get; set; } = null!;
		public string? CompanyName { get; set; } = null!;
		public string? Affiliation { get; set; } = null!;

		public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
		public DateTime? LastLogin { get; set; }


		public List<RefreshToken> RefreshTokens { get; set; } = new();
		public virtual List<UserRole> UserRoles { get; set; } = new();

		//Aggregate
		public int CreatedById { get; set; } = default!;
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public int? LastModifiedById { get; set; } = 0;
		public DateTime? LasModifiedOn { get; set; } = DateTime.UtcNow;

		private List<IDomainEvent> _domainEvents = new();
		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
		public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
		public void ClearDomainEvents() => _domainEvents.Clear();
}

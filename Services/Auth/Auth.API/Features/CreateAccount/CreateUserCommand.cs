using Auth.Domain;

namespace Auth.API.Features;

public record CreateUserCommand
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
		public required Gender Gender { get; set; }

		public string PhoneNumber { get; set; } = default!;
    public string PhotoUrl { get; set; } = default!;
		public string CompanyName { get; set; } = default!;
		public string Position { get; set; } = default!;

    public List<UserRoleDto> UserRoles { get; set; } = new();

    public int? EmployeeId { get; set; }
    public bool IsSuperUser { get; set; }
}

public record UserRoleDto
{
		public UserRoleType Type { get; set; }
		public DateTime? BeginDate { get; set; }
		public DateTime? ExpiringDate { get; set; }
}

public record CreateUserResponse(string Email, int UserId, string Token);

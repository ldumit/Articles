using Articles.Abstractions.Enums;
using Auth.Domain.Users.Enums;

namespace Auth.Domain.Users;

public interface IUserCreationInfo
{
		string Email { get; }
		string FirstName { get; }
		string LastName { get; }
		Gender Gender { get; }

		Honorific? Honorific { get; }
		string? PhoneNumber { get; }
		string? PictureUrl { get; }
		string? CompanyName { get; }
		string? Position { get;	 }
		string? Affiliation { get; }

		IReadOnlyList<IUserRole> UserRoles { get; }

		int? EmployeeId { get; }
		bool IsSuperUser { get; }
}

public interface IUserRole
{
    public UserRoleType Type { get; }
    public DateTime? StartDate { get; }
    public DateTime? ExpiringDate { get; }
}


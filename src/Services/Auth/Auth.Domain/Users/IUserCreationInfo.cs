using Articles.Abstractions.Enums;
using Auth.Domain.Persons.Enums;

namespace Auth.Domain.Users;

public interface IPersonCeationInfo
{
		string Email { get; }
		string FirstName { get; }
		string LastName { get; }
		Gender Gender { get; }

		Honorific? Honorific { get; }
		string? PictureUrl { get; }
		string? CompanyName { get; }
		string? Position { get; }
		string? Affiliation { get; }
}
public interface IUserCreationInfo : IPersonCeationInfo
{
		string? PhoneNumber { get; }
		IReadOnlyList<IUserRole> UserRoles { get; }

		int? EmployeeId { get; }
		bool IsSuperUser { get; }
}

public interface IUserRole
{
    public UserRoleType RoleId{ get; }
    public DateTime? StartDate { get; }
    public DateTime? ExpiringDate { get; }
}


using Auth.Domain;


namespace Auth.API.Features;


public class UserRoleDto
{
		public UserRoleType Type { get; set; }
		public DateTime? BeginDate { get; set; }
		public DateTime? ExpiringDate { get; set; }
}

public class CreateUserCommand
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nickname { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public string PhotoUrl { get; set; }
		public string CompanyName { get; set; }
		public string Position { get; set; }

		public List<UserRoleDto> Roles { get; set; }

    public Guid? EmployeeId { get; set; }

    public bool IsSuperUser { get; set; }
}

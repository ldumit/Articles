using Articles.Security;

namespace Auth.Domain.Users;

public partial class UserRole
{
		public static UserRole Create(UserRoleType type, DateTime? startDate, DateTime? expiringDate)
		{
				var now = DateTime.UtcNow.Date;

				if (startDate.HasValue && startDate.Value.Date < now)
						throw new ArgumentException("Start date must be today or in the future.", nameof(startDate));

				if (expiringDate.HasValue && startDate.HasValue &&
						startDate.Value.Date >= expiringDate.Value.Date)
						throw new ArgumentException("Expiring date must be after the start date.", nameof(expiringDate));

				var userRole = new UserRole()
				{
						RoleId = (int)type,
						//Type = type,
						StartDate = startDate,
						ExpiringDate = expiringDate
				};

				return userRole;
		}
}

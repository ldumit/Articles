using Articles.Abstractions;
using Mapster;

namespace Auth.Domain.Users;

public partial class UserRole
{
		public static UserRole Create(IUserRole userRoleInfo)
		{
				var now = DateTime.UtcNow.Date;

				if (userRoleInfo.StartDate.HasValue && userRoleInfo.StartDate.Value.Date < now)
						throw new ArgumentException("Start date must be today or in the future.", nameof(userRoleInfo.StartDate));

				if (userRoleInfo.ExpiringDate.HasValue && userRoleInfo.StartDate.HasValue &&
						userRoleInfo.StartDate.Value.Date >= userRoleInfo.ExpiringDate.Value.Date)
						throw new ArgumentException("Expiring date must be after the start date.", nameof(userRoleInfo.ExpiringDate));

				var userRole = userRoleInfo.Adapt<UserRole>();
				return userRole;
		}
}

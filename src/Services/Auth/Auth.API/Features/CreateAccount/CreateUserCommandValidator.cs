using FluentValidation;

namespace Auth.API.Features;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
		public CreateUserCommandValidator()
		{
				RuleFor(c => c.Email).NotEmpty().EmailAddress();
				RuleFor(c => c.FirstName).NotEmpty();
				RuleFor(c => c.LastName).NotEmpty();
				RuleFor(c => c.UserRoles).NotEmpty().WithMessage("User needs at least one role");
				RuleFor(c => c.UserRoles).Must((c, roles) => ValidateUserRoles(roles)).WithMessage("Invalid Role");
		}

		public bool ValidateUserRoles(List<UserRoleDto> roles)
		{
				return roles.Any(role => role.BeginDate.HasValue && role.ExpiringDate.HasValue && role.BeginDate.Value.Date > role.ExpiringDate.Value.Date ||
															role.BeginDate.HasValue && role.BeginDate.Value.Date < DateTime.Now.Date ||
															role.ExpiringDate.HasValue && role.ExpiringDate.Value.Date < DateTime.Now.Date);
		}
}

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
		}
}

using FluentValidation;

namespace Auth.API.Features;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
		public LoginCommandValidator()
		{
				RuleFor(c => c.Email).NotEmpty().EmailAddress();
				RuleFor(c => c.Password).NotEmpty();
		}
}

using FluentValidation;

namespace Auth.API.Features.CreateAccount
{
		public class CreateUserAccountValidator : AbstractValidator<CreateUserCommand>
		{
				public CreateUserAccountValidator() 
				{
						RuleFor(c=> c.Email)
								.NotEmpty().WithMessage("Email address is required")
								.EmailAddress().WithMessage("A valid email is required");

						RuleFor(c => c.FirstName)
								.NotEmpty().WithMessage("FirstName address is required");
						RuleFor(c => c.LastName)
								.NotEmpty().WithMessage("LastName address is required");

						//RuleFor(c => c.PhoneNumber).
						//		PhoneEnumber().WithMessage("Email address is required");

				}
		}
}

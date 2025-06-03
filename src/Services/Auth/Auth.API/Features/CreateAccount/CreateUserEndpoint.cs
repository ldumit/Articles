using Blocks.Exceptions;
using EmailService.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Auth.Domain.Users.Events;

using User = Auth.Domain.Users.User;

namespace Auth.API.Features;

//[AllowAnonymous]
[Authorize(Roles = Articles.Security.Role.ADMIN)]
//[Authorize()]
[HttpPost("users")]
public class CreateUserEndpoint(UserManager<User> userManager, AutoMapper.IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IOptions<EmailOptions> emailOptions) 
		: Endpoint<CreateUserCommand, CreateUserResponse>
{
		//public override void Configure()
		////{
		//		Post("users");
		//		Roles(Articles.Security.Role.ADMIN); // same as [Authorize(Roles=...)]
		//}

		public override async Task HandleAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = await userManager.FindByNameAsync(command.Email);
        if(user != null)
						throw new BadRequestException($"User with email {command.Email} already exists");

				user = Auth.Domain.Users.User.Create(command);

				var result = await userManager.CreateAsync(user);
				if (!result.Succeeded)
				{
						var errorMessages = string.Join(" | ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
						throw new BadRequestException($"Unable to create user: {errorMessages}");
				}

				var ressetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

				await PublishAsync(new UserCreated(user, ressetPasswordToken));

				await SendAsync(new CreateUserResponse(command.Email, user.Id, ressetPasswordToken));
    }
}

using Blocks.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Auth.Domain.Users.Events;

namespace Auth.API.Features;

[Authorize(Roles = Articles.Security.Role.ADMIN)]
[HttpPost("users")]
public class CreateUserEndpoint(UserManager<User> userManager) 
		: Endpoint<CreateUserCommand, CreateUserResponse>
{
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

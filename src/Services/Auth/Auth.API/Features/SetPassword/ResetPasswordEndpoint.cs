using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Auth.Domain.Users;
using Blocks.Exceptions;

namespace Auth.API.Features;

[AllowAnonymous]
[HttpPost("set-first-password", "reset-password")]
public class ResetPasswordEndpoint(UserManager<User> _userManager) 
		: Endpoint<SetPasswordCommand, SetPasswordResponse>
{
		public override async Task HandleAsync(SetPasswordCommand command, CancellationToken ct)
		{
				var user = await _userManager.FindByEmailAsync(command.Email);
				if (user == null)
						throw new BadRequestException($"User with email {command.Email} doesn't exist");
				
				var result = await _userManager.ResetPasswordAsync(user, command.TwoFactorToken, command.NewPassword);
				if (!result.Succeeded)
						throw new BadRequestException($"Unable to change password for {command.Email}");

				await SendAsync(new SetPasswordResponse(command.Email));
		}
}
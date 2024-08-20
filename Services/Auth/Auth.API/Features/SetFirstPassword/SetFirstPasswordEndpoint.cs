using Microsoft.AspNetCore.Authorization;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Auth.Application;
using Microsoft.Extensions.Options;
using Auth.Domain.Models;
using Articles.Exceptions;
using Articles.System;
using System.Web;

namespace Auth.API.Features;

public record SetFirstPasswordCommand(string Email, string NewPassword, string ConfirmPassword, string TwoFactorToken);
public record SetFirstPasswordResponse(string EmailAddress);

[AllowAnonymous]
[HttpPost("set-first-password")]
public class SetFirstPasswordEndpoint(UserManager<User> _userManager, IOptions<JwtOptions> _jwtOptions) 
		: Endpoint<SetFirstPasswordCommand, SetFirstPasswordResponse>
{
		public override async Task HandleAsync(SetFirstPasswordCommand command, CancellationToken ct)
		{
				//todo fluentvalidation
				if (command.NewPassword != command.ConfirmPassword)
						throw new BadRequestException($"Password for {command.Email} doesn't match");

				var user = await _userManager.FindByEmailAsync(command.Email);
				if (user == null)
						throw new BadRequestException($"User with email {command.Email} doesn't exist");

				if (!user.PasswordHash.IsNullOrEmpty())
						throw new BadRequestException("link is not available anymore, user already has a password defined");

				var decodedToken = HttpUtility.UrlDecode(command.TwoFactorToken);

				var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
				if (!result.Succeeded)
						throw new BadRequestException($"Unable to confirm email {command.Email}");
				
				result = await _userManager.AddPasswordAsync(user, command.NewPassword);
				if (!result.Succeeded)
						throw new BadRequestException($"Unable to change password for{command.Email}");

				await SendAsync(new SetFirstPasswordResponse(command.Email));
		}
}
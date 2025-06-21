using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Blocks.Exceptions;
using Auth.Application;

namespace Auth.API.Features;

[AllowAnonymous]
[HttpPost("login")]
public class LoginEndpoint(UserManager<User> _userManager, SignInManager<User> _signInManager, TokenFactory _tokenFactory) 
		: Endpoint<LoginCommand, LoginResponse>
{
		public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
		{
				var user = await _userManager.FindByEmailAsync(command.Email);
				if (user is null)
						throw new BadRequestException($"User not found {command.Email}");

				var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: false); // turn lockoutOnFailure to true if you want to block the account after a few tries
				if (!result.Succeeded)
						throw new BadRequestException($"Invalid credentials for {command.Email}");

				var userRoles = await _userManager.GetRolesAsync(user);

				var jwtToken = _tokenFactory.GenerateJWTToken(user.Id.ToString(), user.FullName, command.Email, userRoles, Array.Empty<Claim>());				
				var refreshToken = _tokenFactory.GenerateRefreshToken();

				user.AssignRefreshToken(refreshToken);
				await _userManager.UpdateAsync(user);

				await SendAsync(new LoginResponse(command.Email, jwtToken, refreshToken.Token));
		}
}

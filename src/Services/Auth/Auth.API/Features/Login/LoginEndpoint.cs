using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Auth.Domain.Users;
using Articles.Security;
using Blocks.Security;

namespace Auth.API.Features;

[AllowAnonymous]
[HttpPost("login")]
public class LoginEndpoint(UserManager<User> _userManager, SignInManager<User> _signInManager, TokenFactory _tokenFactory, IOptions<JwtOptions> _jwtOptions) 
		: Endpoint<LoginCommand, LoginResponse>
{
		public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
		{
				var user = await _userManager.FindByEmailAsync(command.Email);
				if (user is null)
						ThrowError("User not found", (int)HttpStatusCode.BadRequest);

				var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: false);
				if (!result.Succeeded) 
						ThrowError("Invalid credentials", (int) HttpStatusCode.BadRequest);

				var userRoles = await _userManager.GetRolesAsync(user);

				var jwtToken = _tokenFactory.GenerateJWTToken(user.Id.ToString(), user.FullName, command.Email, userRoles, Array.Empty<Claim>());
				
				var refreshToken = _tokenFactory.GenerateRefreshToken();
				user.RefreshTokens.Add(refreshToken);
				await _userManager.UpdateAsync(user);

				await SendAsync(new LoginResponse(command.Email, jwtToken, refreshToken.Token));
		}
}

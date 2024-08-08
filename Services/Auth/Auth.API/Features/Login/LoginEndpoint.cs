using Microsoft.AspNetCore.Authorization;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Auth.Application;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Auth.Domain;

namespace Auth.API.Features
{
		public record LoginCommand(string Email, string Password);
		public record LoginResponse(string JWTToken, string RefreshToken);

		[AllowAnonymous]
		[HttpPut("{articleId:int}/typesetter")]
		public class LoginEndpoint(UserManager<User> _userManager, IOptions<JwtOptions> _jwtOptions) : Endpoint<LoginCommand, LoginResponse>
		{
				public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
				{
						var user = await _userManager.FindByEmailAsync(command.Email);

						if (user is null || (await _userManager.CheckPasswordAsync(user, command.Password)) == false)
						{
								ThrowError("Invalid credentials", (int) HttpStatusCode.Unauthorized);
						}


						await SendAsync(new LoginResponse(
								GenerateJWTToken(user.Id.ToString(), user.Email, Array.Empty<Claim>()),
								string.Empty));

				}

				public string GenerateJWTToken(string userId, string email, IEnumerable<Claim> additionalClaims)
				{
						var claims = new[]
						{
								new Claim(JwtRegisteredClaimNames.Sub, userId),
								new Claim(JwtRegisteredClaimNames.Email, email),
								//new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator()),
								//new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
						}
						.Concat(additionalClaims);

						var jwtSettings = _jwtOptions.Value;
						var jwtToken = new JwtSecurityToken(
								issuer: jwtSettings.Issuer,
								audience: jwtSettings.Audience,
								claims: claims,
								notBefore: DateTime.UtcNow,
								expires: DateTime.UtcNow.Add(jwtSettings.ValidFor))
								//signingCredentials: jwtSettings.SigningCredentials)
								;
						var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
						//var zippedToken = Convert.ToBase64String(ZipHelper.Zip(encodedJwt));
						//var zippedToken = Convert.ToBase64String(encodedJwt);

						return encodedJwt;
				}

		}
}

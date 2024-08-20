using Articles.System;
using Auth.Application;
using Auth.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Articles.Security;

public class TokenFactory
{
		private readonly JwtOptions _jwtOptions;

		public TokenFactory(IOptions<JwtOptions> jwtOptions)
		{
				_jwtOptions = jwtOptions.Value;
		}

		public RefreshToken GenerateRefreshToken()
		{
				using (var rng = RandomNumberGenerator.Create())
				{
						var randomBytes = new byte[64];
						rng.GetBytes(randomBytes);
						return new RefreshToken
						{
								Token = Convert.ToBase64String(randomBytes),
								Expires = DateTime.UtcNow.AddDays(7),
								Created = DateTime.UtcNow,
								CreatedByIp = Network.GetLocalIPAddress(),
						};
				}
		}

		public string GenerateJWTToken(string userId, string email, IList<string> userRoles, IEnumerable<Claim> additionalClaims)
		{
				var secretKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_jwtOptions.Secret));
				var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

				var claims = new[]
				{
						new Claim(JwtRegisteredClaimNames.Sub, userId),
						new Claim(JwtRegisteredClaimNames.Email, email),
						//new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator()),
						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
				}
				.Concat(userRoles.Select(r => new Claim(ClaimTypes.Role, r)))
				.Concat(additionalClaims);

				//talk - explain JWT token properties & the encoding
				var jwtToken = new JwtSecurityToken(
						issuer: _jwtOptions.Issuer,
						audience: _jwtOptions.Audience,
						claims: claims,
						notBefore: DateTime.UtcNow,
						expires: DateTime.UtcNow.Add(_jwtOptions.ValidFor),
						signingCredentials: signinCredentials);

				var encodedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
				return encodedJwtToken;
		}
}

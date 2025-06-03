using Articles.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Auth.API.Features;

//[AllowAnonymous]
[Authorize(Roles = Role.ADMIN)]
[HttpPost("test")]
public class TestEndpoint(IOptions<JwtOptions> _jwtOptions)
		: Endpoint<LoginCommand, LoginResponse>
{
		public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
		{
				await SendAsync(new LoginResponse(command.Email, _jwtOptions.Value.Audience, _jwtOptions.Value.ValidForInMinutes.ToString()));
		}
}
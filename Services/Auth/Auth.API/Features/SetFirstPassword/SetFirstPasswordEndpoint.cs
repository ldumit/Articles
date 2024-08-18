using Microsoft.AspNetCore.Authorization;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Auth.Application;
using Microsoft.Extensions.Options;
using Auth.Domain.Models;

namespace Auth.API.Features;

public record SetFirstPasswordCommand(string EmailAddress, string NewPaasword, string ConfirmPassword, string TwoFactorToken);
public record SetFirstPasswordResponse(string EmailAddress);

[AllowAnonymous]
[HttpPost("set-first-password")]
public class SetFirstPasswordEndpoint(UserManager<User> _userManager, IOptions<JwtOptions> _jwtOptions) 
		: Endpoint<SetFirstPasswordCommand, SetFirstPasswordResponse>
{
		public override async Task HandleAsync(SetFirstPasswordCommand command, CancellationToken ct)
		{
				await SendAsync(new SetFirstPasswordResponse(command.EmailAddress));
		}
}
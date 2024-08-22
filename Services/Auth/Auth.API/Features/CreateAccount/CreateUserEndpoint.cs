using Articles.Exceptions;
using Auth.Domain.Models;
using EmailService.Contracts;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Articles.AspNetCore;
using Flurl;
using Microsoft.Extensions.Options;

namespace Auth.API.Features;

[AllowAnonymous]
[HttpPost("users")]
public class CreateUserEndpoint(UserManager<User> userManager, AutoMapper.IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IOptions<EmailOptions> emailOptions) 
		: Endpoint<CreateUserCommand, CreateUserResponse>
{
		public override async Task HandleAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = await userManager.FindByNameAsync(command.Email);
        //todo - add extensions methods for object (IsNull, IsNotNull)
        if(user != null)
						throw new BadRequestException($"User with email {command.Email} already exists");

        //ValidateUserRoles(command.UserRoles);

        user = mapper.Map<User>(command);

        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
            throw new HttpException(HttpStatusCode.InternalServerError, $"Unable to create user: {result.Errors.ElementAt(0).Description} with code {result.Errors.ElementAt(0).Code}");

				var ressetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

				var emailMessage = BuildEmailMessage(user, ressetPasswordToken);
				await emailService.SendEmailAsync(emailMessage);

				await SendAsync(new CreateUserResponse(command.Email, user.Id, ressetPasswordToken));
    }

		private EmailMessage BuildEmailMessage(User user, string  token)
		{
				const string ConfirmationEmail = 
						@"Dear {0}, An account has been created for you. Please set your password using the following URL: {1}";

				//todo - add presentation application URl to appsettings
				var url = 
						httpContextAccessor.HttpContext?.Request.BaseUrl()
						.AppendPathSegment("set-first-password")
						.SetQueryParams(new { token });

				return new EmailMessage(
						"Confirmation", 
						new Content(ContentType.Html, string.Format(ConfirmationEmail, user.FirstName + " " +user.LastName, url)),
						new EmailAddress("articles", emailOptions.Value.EmailFromAddress),
						new List<EmailAddress> { new EmailAddress(user.FullName, user.Email) }
						);
		}

		public void ValidateUserRoles(List<UserRoleDto> roles)
		{
				if (roles.Any(role => role.BeginDate.HasValue && role.ExpiringDate.HasValue && role.BeginDate.Value.Date > role.ExpiringDate.Value.Date ||
															role.BeginDate.HasValue && role.BeginDate.Value.Date < DateTime.Now.Date ||
															role.ExpiringDate.HasValue && role.ExpiringDate.Value.Date < DateTime.Now.Date))
				{
						throw new BadRequestException("Invalid Role");
				}
		}
}

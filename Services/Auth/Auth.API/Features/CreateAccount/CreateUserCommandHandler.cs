using Articles.Exceptions;
using Auth.Domain;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Auth.API.Features;

public class CreateUserCommandHandler(UserManager<User> userManager, AutoMapper.IMapper mapper) 
		: Endpoint<CreateUserCommand, int>
{
		public override async Task HandleAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = await userManager.FindByNameAsync(command.Email);
        //todo - add extensions methods for object (IsNull, IsNotNull)
        if(user != null)
						throw new BadRequestException($"User with email {command.Email} already exists");

        ValidateUserRoles(command.Roles);

        user = mapper.Map<User>(command);
				//user.UserRoles = command.Roles.Select(r => new UserRole { RoleId = (int) r.Type}).ToList();

        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
            throw new HttpException(HttpStatusCode.InternalServerError, $"Unable to create user: {result.Errors.ElementAt(0).Description} with code {result.Errors.ElementAt(0).Code}");


				await SendAsync(user.Id);
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

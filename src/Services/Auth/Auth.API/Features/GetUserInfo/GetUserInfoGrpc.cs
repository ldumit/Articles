using Microsoft.AspNetCore.Identity;
using Mapster;
using Grpc.Core;
using Blocks.Exceptions;
using Auth.Grpc;
using Auth.Domain.Models;
using Auth.API.Mappings;

namespace Auth.API.Features.GetUserInfo;

public partial class GetUserInfoGrpc(UserManager<User> _userManager, GrpcTypeAdapterConfig _typeAdapterConfig) : AuthService.AuthServiceBase
{
		public override async Task<GetUserResponse> GetUserById(GetUserRequest request, ServerCallContext context)
		{
				var user = await _userManager.FindByIdAsync(request.UserId.ToString());
				if (user == null)
						throw new NotFoundException("User not found");

				return new GetUserResponse
				{
						UserInfo = user.Adapt<UserInfo>(_typeAdapterConfig)
				};
		}
}

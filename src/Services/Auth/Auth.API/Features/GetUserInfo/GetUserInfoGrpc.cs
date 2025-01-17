//using Microsoft.AspNetCore.Http;
//using Grpc.Core;
//using UserService;  
// The generated namespace from the .proto file

namespace Auth.API.Features.GetUserInfo;


//public class GetUserInfoGrpc : UserService.UserServiceBase
//{
//		private readonly IUserRepository _userRepository;

//		public GetUserInfoGrpc(IUserRepository userRepository)
//		{
//				_userRepository = userRepository;
//		}

//		// Implement the GetUserInfo RPC method
//		public override async Task<GetUserResponse> GetUserInfo(GetUserRequest request, ServerCallContext context)
//		{
//				// Retrieve user information based on userId from repository
//				var user = await _userRepository.GetUserByIdAsync(request.UserId);

//				if (user == null)
//				{
//						// Handle user not found (optional: you could throw an exception or return an empty response)
//						throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
//				}

//				// Return the user information as a response
//				return new GetUserResponse
//				{
//						UserInfo = new UserInfo
//						{
//								FullName = user.FullName,
//								Email = user.Email,
//								Affiliation = user.Affiliation
//						}
//				};
//		}
//}

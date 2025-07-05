namespace UserServiceGrpcClient;

//public interface IUserServiceGrpcClient
//{
//		Task<UserInfo> GetUserInfoAsync(string userId);
//}

//public class UserServiceGrpcClient : IUserServiceGrpcClient
//{
//		private readonly UserService.UserServiceClient _userServiceClient;

//		public UserServiceGrpcClient(IHttpClientFactory httpClientFactory)
//		{
//				var client = httpClientFactory.CreateClient("UserServiceClient");
//				_userServiceClient = new UserService.UserServiceClient(client);
//		}

//		public async Task<UserInfo> GetUserInfoAsync(string userId)
//		{
//				var request = new GetPersonRequest { UserId = userId };
//				var response = await _userServiceClient.GetUserInfoAsync(request);
//				return response.UserInfo;
//		}
//}

namespace Auth.API.Features.SendChangePasswordLink
{
		public record SendChangePasswordLinkCommand(string Email);
		public record SendChangePasswordLinkResponse(string Email, string Code);
}

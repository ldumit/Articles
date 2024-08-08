using System.ComponentModel.DataAnnotations;

namespace Auth.API.Features.Login
{
		public record LoginCommand (string Email, string Password);
}

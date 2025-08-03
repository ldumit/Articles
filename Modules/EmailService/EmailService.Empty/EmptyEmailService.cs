using EmailService.Contracts;

namespace EmailService.Empty;

public class EmptyEmailService : IEmailService
{
		public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
		{
				Console.WriteLine($"[EmptyEmailService] Skipped sending email to: {emailMessage.To}");

				return await Task.FromResult(true);
		}
}

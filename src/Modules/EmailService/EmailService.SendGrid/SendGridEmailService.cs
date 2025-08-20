using EmailService.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;

namespace EmailService.SendGrid;

public class SendGridEmailService(ISendGridClient sendGridClient, IOptions<SendGridAccountOptions> sendGridOptions, IOptions<EmailOptions> emailOptions, ILogger<SendGridEmailService> logger) 
    : IEmailService
{
    public SendGridAccountOptions Options { get; }

    public EmailOptions EmailOptions { get; }


		public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
		{
        var msg = emailMessage.ToSendGridMessage();
				msg.SetClickTracking(false, false);

				var response = await sendGridClient.SendEmailAsync(msg);

				if (response == null)
				{
						logger.LogError($"unknow error");
						return false;
				}
				else if (!response.IsSuccessStatusCode)
				{
						logger.LogError($"the mail service returned status code response.StatusCode");
						return false;
				}

				return true;
		}
}

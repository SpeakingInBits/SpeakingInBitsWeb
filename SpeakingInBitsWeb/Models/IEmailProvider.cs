using SendGrid;
using SendGrid.Helpers.Mail;

namespace SpeakingInBitsWeb.Models
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(string toEmail, string fromEmail, string subject, 
                                string content, string htmlContent);
    }

    public class EmailProviderSendGrid : IEmailProvider
    {
        private readonly IConfiguration _config;
        public EmailProviderSendGrid(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string? fromEmail, string subject, string content, string htmlContent)
        {
            var apiKey = _config.GetSection("SendGridKey").Value;
            if (fromEmail == null)
            {
                fromEmail = _config.GetSection("FromEmail").Value;
            }
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, "SpeakingInBits"),
                Subject = subject,
                PlainTextContent =content,
                HtmlContent = htmlContent
            };
            msg.AddTo(new EmailAddress(toEmail, "User name"));
            await client.SendEmailAsync(msg);
            // var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}

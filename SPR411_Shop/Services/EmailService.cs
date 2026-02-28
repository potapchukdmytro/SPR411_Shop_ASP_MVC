using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace SPR411_Shop.Services
{
    public class EmailService : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;

            _fromEmail = "";
            string password = "";
            string host = "smtp.gmail.com";
            int port = 587;

            _smtpClient = new SmtpClient(host, port);
            _smtpClient.Credentials = new NetworkCredential(_fromEmail, password);
            _smtpClient.EnableSsl = true;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessage;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(email);
                mailMessage.From = new MailAddress(_fromEmail);

                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                _logger.LogInformation("Enter password and email for smtp client");
            }
        }
    }
}

using DvdShop.Interface.IServices;
using System.Net;
using System.Net.Mail;

namespace DvdShop.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(IConfiguration configuration)
        {
            _smtpHost = configuration["EmailSettings:SmtpHost"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _smtpUser = configuration["EmailSettings:SmtpUser"];
            _smtpPass = configuration["EmailSettings:SmtpPass"];
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
        {
            var fromAddress = new MailAddress(_smtpUser, "PranavStark");
            var toAddress = new MailAddress(to);

            using var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml // This flag enables HTML formatting for the email body
            };

            try
            {
                await smtpClient.SendMailAsync(message);
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("SMTP error occurred while sending email: " + smtpEx.Message, smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sending email: " + ex.Message, ex);
            }
        }

    }

}

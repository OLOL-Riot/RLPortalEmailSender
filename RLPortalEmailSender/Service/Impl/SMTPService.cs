using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Runtime.CompilerServices;

namespace RLPortalEmailSender.Service.Impl
{
    public class SMTPService : ISMTPService
    {

        private readonly IOptions<EmailOptions> _emailOptions;

        public SMTPService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions;
        }

        private SmtpClient CreateSmtpClient()
        {
            var email = _emailOptions.Value;
            var smtp = Activator.CreateInstance<SmtpClient>();
            smtp.Connect(email.Host, email.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(email.MailAdress, email.Password);
            return smtp;
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            var smtp = CreateSmtpClient();
            await smtp.SendAsync(message);
            smtp.Disconnect(true);
        }


    }
}

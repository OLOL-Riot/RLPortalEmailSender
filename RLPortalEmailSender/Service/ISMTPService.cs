using MailKit.Net.Smtp;
using MimeKit;

namespace RLPortalEmailSender.Service
{
    public interface ISMTPService
    {
        public Task SendEmailAsync(MimeMessage message);
    }
}

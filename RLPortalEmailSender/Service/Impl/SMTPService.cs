using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace RLPortalEmailSender.Service.Impl
{
    public class SMTPService : ISMTPService
    {

        private static SmtpClient CreateSmtpClient()
        {
            var smtp = Activator.CreateInstance<SmtpClient>();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("geography.pet.project.mail.sender@gmail.com", "obudipblxgeqrnxk");
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

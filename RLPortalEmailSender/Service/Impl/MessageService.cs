using RLPortalBackend.Container.Messages;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Flows;

namespace EmailSender.Service.Impl
{
    /// <summary>
    /// Message Service
    /// </summary>
    public class MessageService : IMessageService
    {

        private readonly ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public async Task SendMessege(MessageToSend message)
        {
            _logger.LogInformation(message.ToString());
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!Regex.IsMatch(message.EmailAdress, pattern, RegexOptions.IgnoreCase) | message.Subject == null | message.TextOfEmail == null)
                throw new ArgumentException();

            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse("summer.dietrich@ethereal.email"));
            mail.To.Add(MailboxAddress.Parse(message.EmailAdress));
            mail.Subject = message.Subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = message.TextOfEmail };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("geography.pet.project.mail.sender@gmail.com", "obudipblxgeqrnxk");
            try
            {
                await client.SendAsync(mail);
                _logger.LogInformation("Successfully sending");
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }

            client.Disconnect(true);
            

        }

    }
}

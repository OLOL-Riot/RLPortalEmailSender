using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using RLPortalBackend.Container.Messages;
using System.Text.RegularExpressions;

namespace RLPortalEmailSender.Service.Impl
{
    /// <summary>
    /// Message Service
    /// </summary>
    public class MessageService : IMessageService
    {

        private readonly ILogger<MessageService> _logger;

        private readonly ISMTPService _smtp;

        public MessageService(ILogger<MessageService> logger, ISMTPService smtp)
        {
            _logger = logger;
            _smtp = smtp;
        }

        public async Task SendMessege(MessageToSend message)
        {
            _logger.LogInformation(message.ToString());
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!Regex.IsMatch(message.EmailAdress, pattern, RegexOptions.IgnoreCase) | message.Topic == null | message.TextOfEmail == null)
                throw new ArgumentException();

            var mail = MakeMessage(message);
            
            try
            {
                 await _smtp.SendEmailAsync(mail);
                _logger.LogInformation($"{DateTime.UtcNow} | Sended email on {mail.To} Subject:{mail.Subject} Body:{mail.Body}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }

        }

        private MimeMessage MakeMessage(MessageToSend message)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse("RlPortal"));
            mail.To.Add(MailboxAddress.Parse(message.EmailAdress));
            mail.Subject = message.Topic;
            mail.Body = new TextPart(TextFormat.Html) { Text = message.TextOfEmail };
            return mail;
        }
    }
}

using EmailSender.Service;
using Microsoft.AspNetCore.Mvc;
using RLPortalBackend.Container.Messages;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class MailSenderController : ControllerBase
    {

        private readonly ILogger<MailSenderController> _logger;

        private readonly IMessageService _messageService;

        public MailSenderController(ILogger<MailSenderController> logger, IMessageService messageService)
        {
            _messageService = messageService;
            _logger = logger;
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost(Name = "SendMail")]
        public IActionResult SendMessage(MessageToSend data)
        {
            try
            {
                _messageService.SendMessege(data);
                _logger.LogInformation("Successfully sending");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
                return BadRequest("Non valid email");
            }
        }
    }
}
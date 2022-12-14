using RLPortalEmailSender.Service;
using RLPortalBackend.Container.Messages;
using MassTransit;

namespace RLPortalEmailSender.Container.Consumers
{
    public class MessageToSendConsumer : IConsumer<MessageToSend>
    {

        private readonly IMessageService _messageService;

        readonly ILogger<MessageToSendConsumer> _logger;

        public MessageToSendConsumer(IMessageService messageService, ILogger<MessageToSendConsumer> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MessageToSend> context)
        {
            await _messageService.SendMessege(context.Message);


        }
    }
}

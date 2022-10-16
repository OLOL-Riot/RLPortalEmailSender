using RLPortalBackend.Container.Messages;

namespace RLPortalEmailSender.Service
{
    /// <summary>
    /// Interface for Message Service
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message"></param>
        public Task SendMessege(MessageToSend message);
    }
}

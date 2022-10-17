namespace RLPortalBackend.Container.Messages
{
    /// <summary>
    /// Message
    /// </summary>
    public class MessageToSend
    {
        /// <summary>
        /// Message constuctor
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <param name="textOfEmail"></param>
        /// <param name="topic"></param>
        public MessageToSend(string emailAdress, string topic, string textOfEmail)
        {
            EmailAdress = emailAdress;
            Topic = topic;
            TextOfEmail = textOfEmail;
        }

        /// <summary>
        /// Mail adress
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// Article
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string TextOfEmail { get; set; }





    }
}
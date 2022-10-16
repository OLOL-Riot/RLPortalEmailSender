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
        /// <param name="article"></param>
        public MessageToSend(string emailAdress, string article, string textOfEmail)
        {
            EmailAdress = emailAdress;
            Article = article;
            TextOfEmail = textOfEmail;
        }

        /// <summary>
        /// Mail adress
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// Article
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string TextOfEmail { get; set; }





    }
}
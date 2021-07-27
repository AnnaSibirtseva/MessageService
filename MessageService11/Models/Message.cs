using System.Runtime.Serialization;

namespace MessageService11.Models
{
    /// <summary>
    /// Message class.
    /// </summary>
    [DataContract]
    public class Message
    {
        /// <summary>
        /// Message topic.
        /// </summary>
        [DataMember]
        public string Topic { get; set; }
        /// <summary>
        /// Message text.
        /// </summary>
        [DataMember]
        public string Text { get; set; }
        /// <summary>
        /// Sender of the message.
        /// </summary>
        [DataMember]
        public string SenderID { get; set; }
        /// <summary>
        /// Receiver of the message.
        /// </summary>
        [DataMember]
        public string ReceiverID { get; set; }
        public Message(string topic, string text, string sender, string receiver)
        {
            Topic = topic;
            Text = text;
            SenderID = sender;
            ReceiverID = receiver;
        }
        /// <summary>
        /// Empty constructor for the serialization.
        /// </summary>
        public Message()
        {

        }
    }
}

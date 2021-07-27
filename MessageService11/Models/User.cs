using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MessageService11.Models
{
    /// <summary>
    /// User class.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// User's name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// User's email address.
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// All user's messages.
        /// </summary>
        [DataMember]
        public List<Message> Messages { get; set; } = new List<Message>();
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
        /// <summary>
        /// Empty constructor for the serialization.
        /// </summary>
        public User()
        {

        }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MessageService11.Models
{
    /// <summary>
    /// Class for serializing/deserializing.
    /// </summary>
    public class Serializer
    {
        /// <summary>
        /// Serializing all users.
        /// </summary>
        /// <param name="AllUsers">All users list.</param>
        /// <param name="AllUsersNames">All user's names list.</param>
        public static void SerializeUsers(List<User> AllUsers, List<string> AllUsersNames)
        {
            // Cleaning up the file before writing enything.
            if (File.Exists("SavedThings.json"))
            {
                StreamWriter writer = new StreamWriter("SavedThings.json");
                writer.WriteLine("");
                writer.Close();
            }
            // Writing all users in the .json file.
            using (StreamWriter streamWriter = new StreamWriter(new FileStream("SavedThings.json", FileMode.Create)))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                // Saving users in the lexicographical order. 
                jsonSerializer.Serialize(streamWriter, AllUsers.OrderBy(x => x.Email).ToList());
            }
        }
        /// <summary>
        /// Serializing all the messages.
        /// </summary>
        /// <param name="AllMessages">All messages list.</param>
        public static void SerializeMessages(List<Message> AllMessages)
        {
            // Cleaning up the file before writing enything.
            if (File.Exists("SavedMessages.json"))
            {
                StreamWriter writer = new StreamWriter("SavedMessages.json");
                writer.WriteLine("");
                writer.Close();
            }
            // Writing all messages in the .json file.
            using (StreamWriter streamWriter = new StreamWriter(new FileStream("SavedMessages.json", FileMode.Create)))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(streamWriter, AllMessages);
            }
        }
        /// <summary>
        /// Deserializing all users.
        /// </summary>
        /// <param name="AllUsers">All users names list.</param>
        public static void DeserializeUsers(out List<User> AllUsers)
        {
            // Reading from the .json file.
            using (StreamReader streamReader = new StreamReader(new FileStream("SavedThings.json", FileMode.Open)))
            {
                string savedData = streamReader.ReadToEnd();
                var cSavedThings = JsonConvert.DeserializeObject<List<User>>(savedData);
                // Assign new values to the users in the list.
                AllUsers = cSavedThings;
            }
        }
        /// <summary>
        /// Deserializing all messages.
        /// </summary>
        /// <param name="AllMessages">All messages list.</param>
        public static void DeserializeMessages(out List<Message> AllMessages)
        {
            // Reading from the .json file.
            using (StreamReader streamReader = new StreamReader(new FileStream("SavedMessages.json", FileMode.Open)))
            {
                string savedData = streamReader.ReadToEnd();
                var cSavedThings = JsonConvert.DeserializeObject<List<Message>>(savedData);
                // Assign new values to the meassages in the list.
                AllMessages = cSavedThings;
            }
        }
    }
}

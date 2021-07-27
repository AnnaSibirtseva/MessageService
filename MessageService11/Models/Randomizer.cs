using System;
using System.Collections.Generic;
using System.IO;

namespace MessageService11.Models
{
    /// <summary>
    /// Class for creating random users.
    /// </summary>
    public class Randomizer
    {
        /// <summary>
        /// Method for creating random messages for the users.
        /// </summary>
        /// <param name="AllUsersNames">All user's names list.</param>
        /// <param name="AllMessages">All messages list.</param>
        /// <param name="AllUsers">All users list.</param>
        public static void CreateRandomMessage(List<string> AllUsersNames, ref List<Message> AllMessages, ref List<User> AllUsers)
        {
            Random randomic = new Random();
            for (int i = 0; i < AllUsersNames.Count; i++)
            {
                // Creating random number of messages.
                int m = randomic.Next(2, 10);
                for (int j = 0; j < m; j++)
                {
                    // Reading a .txt file with random sentences. 
                    using (StreamReader streamReader = new StreamReader("ForRandom" + Path.DirectorySeparatorChar + "Messages.txt"))
                    {
                        string[] messages = streamReader.ReadToEnd().Split('\n');
                        // Reading a .txt file with random topics for the message. 
                        using (StreamReader sr = new StreamReader("ForRandom" + Path.DirectorySeparatorChar + "Topics.txt"))
                        {
                            string[] topics = sr.ReadToEnd().Split('\n');
                            // Creating new message.
                            Message message = new Message(topics[randomic.Next(0, topics.Length - 1)].Trim('\r'), 
                                messages[randomic.Next(0, messages.Length - 1)].Trim('\r').Trim('\t'), AllUsers[i].Email, 
                                AllUsers[randomic.Next(0, AllUsers.Count - 1)].Email);
                            // Adding it to everywhere needed.
                            AllMessages.Add(message);
                            AllUsers[i].Messages.Add(message);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Method for creating a new random user's.
        /// </summary>
        /// <param name="AllUsersNames">All user's names list.</param>
        /// <param name="AllUsers">All users list.</param>
        public static void CreateRandomUser(List<string> AllUsersNames, ref List<User> AllUsers)
        {
            Random randomic = new Random();
            // Creating a random number for the amount of users.
            int n = randomic.Next(3, 28);
            string NameForUser = "";
            for (int i = 0; i < n; i++)
            {
                while (NameForUser == "")
                {
                    // Reading a .txt file with random names. 
                    using (StreamReader streamReader = new StreamReader("ForRandom" + Path.DirectorySeparatorChar + "Names.txt"))
                    {
                        string[] names = streamReader.ReadToEnd().Split('\n');
                        // Chosing a random name.
                        int ind = randomic.Next(0, 110);
                        // If user with thic name already exist, then we try again.
                        if (AllUsersNames.Count != 0 && AllUsersNames.Contains(names[ind]))
                        {
                            NameForUser = "";
                        }
                        else
                        {
                            NameForUser = names[ind].Trim('\r');
                        }
                    }
                }
                // Creating new user.
                User user = new User(NameForUser, $"{NameForUser.ToLower()}@mail.ru");
                NameForUser = "";
                // Adding him/her to everywhere needed.
                AllUsers.Add(user);
                AllUsersNames.Add(user.Name);
            }
        }
    }
}

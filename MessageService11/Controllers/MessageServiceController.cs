using MessageService11.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApp.Controllers
{
    [ApiController]
    public class MessageServiceController : ControllerBase
    {
        public List<User> AllUsers = new List<User>();
        public List<string> AllUsersNames = new List<string>();
        public List<Message> AllMessages = new List<Message>();
        /// <summary>
        /// Creates random number of users.
        /// </summary>
        /// <returns>List of names of created users.</returns>
        [HttpPost("users")]
        public async Task<IEnumerable<string>> Post()
        {
            // Calling mrthods for creating new random users.
            Randomizer.CreateRandomUser(AllUsersNames, ref AllUsers);
            Randomizer.CreateRandomMessage(AllUsersNames, ref AllMessages, ref AllUsers);
            // Saving all created users in .json file.
            Serializer.SerializeUsers(AllUsers, AllUsersNames);
            Serializer.SerializeMessages(AllMessages);
            // Displaying all created users in lexicographic order.
            return AllUsersNames.OrderBy(x => x.First()).ToList();
        }
        /// <summary>
        /// Displays all users and information about them.
        /// </summary>
        /// <returns>All users information.</returns>
        [HttpGet("allusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            // Recreating all users list.
            Serializer.DeserializeUsers(out AllUsers);
            // Returns all users.
            return AllUsers;
        }
        /// <summary>
        /// Shows all information about chosen user.
        /// </summary>
        /// <param name="email">User's email (name + @mail.ru).</param>
        /// <returns>Chosen user information.</returns>
        /// <response code="404">There is no such user.</response>
        [HttpGet("users/{email}")]
        public async Task<ActionResult<User>> GetUserInfo([FromRoute] string email)
        {
            // Recreating all users list.
            Serializer.DeserializeUsers(out AllUsers);
            var user = AllUsers.Where(x => x.Email == email).ToList();
            // Returns an error code 404 if there is no such user.
            if (user.Count == 0)
            {
                return NotFound();
            }
            return user.First();
        }
        /// <summary>
        /// Displays all messages with such sender email.
        /// </summary>
        /// <param name="sender_email">Sender email (name + @mail.ru).</param>
        /// <returns>All messages with chosen sender.</returns>
        /// <response code="404">There is no such user.</response>
        [HttpGet("msg/users/{sender_email}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetSenderMessage([FromRoute] string sender_email)
        {
            // Recreating all users list.
            Serializer.DeserializeUsers(out AllUsers);
            // Recreating all messages list.
            Serializer.DeserializeMessages(out AllMessages);
            // Finding user with entered email.
            var sender = AllUsers.Where(x => x.Email == sender_email).ToList();
            List<Message> messages = new List<Message>();
            // Returns an error code 404 if there is no such user.
            if (sender.Count == 0)
            {
                return NotFound();
            }
            // Finding all the messages with chosen sender.
            for (int i = 0; i < AllMessages.Count; i++)
            {
                if (AllMessages[i].SenderID == sender_email)
                {
                    messages.Add(AllMessages[i]);
                }
            }
            return messages;
        }
        /// <summary>
        /// Displays all the messages between to chosen people.
        /// </summary>
        /// <param name="sender_email">Sender email (name + @mail.ru).</param>
        /// <param name="receiver_email">Receiver email (name + @mail.ru).</param>
        /// <returns>All messages between to people.</returns>
        /// <response code="404">There is no such users.</response>
        [HttpGet("msg/{sender_email}/{receiver_email}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessage([FromRoute] string sender_email, [FromRoute] string receiver_email)
        {
            // Recreating all users list.
            Serializer.DeserializeUsers(out AllUsers);
            // Recreating all messages list.
            Serializer.DeserializeMessages(out AllMessages);
            // Finding needed users.
            var sender = AllUsers.Where(x => x.Email == sender_email).ToList();
            var receiver = AllUsers.Where(x => x.Email == receiver_email).ToList();
            List<Message> messages = new List<Message>();
            // Returns an error code 404 if there is no such users.
            if (sender.Count == 0 || receiver.Count == 0)
            {
                return NotFound();
            }
            // Finding all the messages between them.
            for (int i = 0; i < AllMessages.Count; i++)
            {
                if (AllMessages[i].ReceiverID == receiver_email && AllMessages[i].SenderID == sender_email)
                {
                    messages.Add(AllMessages[i]);
                }
            }
            return messages;
        }
        /// <summary>
        /// Displays all the messages where receiver is entered user.
        /// </summary>
        /// <param name="receiver_email">Receiver email (name + @mail.ru).</param>
        /// <returns>All messages with chosen receiver.</returns>
        /// <response code="404">There is no such user.</response>
        [HttpGet("msg/{receiver_email}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetReceiverMessage([FromRoute] string receiver_email)
        {
            // Recreating all users list.
            Serializer.DeserializeUsers(out AllUsers);
            // Recreating all messages list.
            Serializer.DeserializeMessages(out AllMessages);
            var receiver = AllUsers.Where(x => x.Email == receiver_email).ToList();
            List<Message> messages = new List<Message>();
            // Returns an error code 404 if there is no such users.
            if (receiver.Count == 0)
            {
                return NotFound();
            }
            // Finding all the messages with chosen receiver.
            for (int i = 0; i < AllMessages.Count; i++)
            {
                if (AllMessages[i].ReceiverID == receiver_email)
                {
                    messages.Add(AllMessages[i]);
                }
            }
            return messages;
        }
        /// <summary>
        /// Creating a new message.
        /// </summary>
        /// <param name="topic">Message topic.</param>
        /// <param name="message">Message text.</param>
        /// <param name="sender_email">Sender email (name + @mail.ru).</param>
        /// <param name="receiver_email">Receiver email (name + @mail.ru).</param>
        /// <returns>Created message.</returns>
        /// <response code="404">There is no such users.</response>
        [HttpPost("msg/{topic}/{message}/{receiver_email}/{sender_email}")]
        public async Task<ActionResult<Message>> MyHandlerPost([FromRoute] string topic, [FromRoute] string message, [FromRoute] string sender_email, [FromRoute] string receiver_email)
        {
            // Recreating all users list.
            Serializer.DeserializeUsers(out AllUsers);
            var sender = AllUsers.Where(x => x.Email == sender_email).ToList();
            var receiver = AllUsers.Where(x => x.Email == receiver_email).ToList();
            List<Message> messages = new List<Message>();
            // Returns an error code 404 if there is no such users.
            if (sender.Count == 0 || receiver.Count == 0)
            {
                return NotFound();
            }
            // Creating a new message.
            Message createdMessage = new Message(topic, message, sender_email, receiver_email);
            sender.First().Messages.Add(createdMessage);
            receiver.First().Messages.Add(createdMessage);
            AllMessages.Add(createdMessage);
            // Saving all created users in .json file.
            Serializer.SerializeUsers(AllUsers, AllUsersNames);
            Serializer.SerializeMessages(AllMessages);
            return createdMessage;
        }
    }
}
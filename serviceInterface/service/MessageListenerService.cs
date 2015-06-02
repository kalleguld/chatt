using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelInterface;

namespace serviceInterface.service
{
    public class MessageListenerService
    {
        public delegate void MessageCreated(int messageId, string username);

        private static readonly IDictionary<string, MessageCreated> MessageCreatedDelegate 
            = new Dictionary<string, MessageCreated>();

        private readonly IList<IMessage> _pendingMessages = new List<IMessage>(); 

        public void AddMessageCreatedDelegate(IToken token, MessageCreated del)
        {
            if (MessageCreatedDelegate.ContainsKey(token.User.Username))
            {
                MessageCreatedDelegate[token.User.Username] += del;
            }
            else
            {
                MessageCreatedDelegate[token.User.Username] = del;
            }
        }

        internal void QueueMessage(IMessage message)
        {
            _pendingMessages.Add(message);
        }

        internal void DatabaseSaved()
        {
            var messageIds = new Dictionary<string, IList<int>>();
            foreach (var message in _pendingMessages)
            {
                var users = (message.Receiver.Username == message.Sender.Username)
                            ? new[] {message.Receiver}
                            : new[] {message.Sender, message.Receiver};

                foreach (var user in users)
                {
                    if (!messageIds.ContainsKey(user.Username))
                    {
                        messageIds[user.Username] = new List<int>();
                    }
                    var msgList = messageIds[user.Username];
                    msgList.Add(message.Id);
                }
            }

            new Task(() =>
            {
                foreach (var entry in messageIds)
                {
                    if (!MessageCreatedDelegate.ContainsKey(entry.Key)) continue;
                    foreach (var messageId in entry.Value)
                    {
                        MessageCreatedDelegate[entry.Key](messageId, entry.Key);
                    }
                }
            }).Start();
        }
    }

}

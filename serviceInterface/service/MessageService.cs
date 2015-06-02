using System;
using System.Collections.Generic;
using System.Linq;
using backend.model;
using modelInterface;

namespace serviceInterface.service
{
    public class MessageService : BaseService
    {
        internal MessageService(Connection connection) : base(connection) { }

        public IMessage GetMessage(IToken token, int messageId)
        {
            var message = Connection.Context.Messages.FirstOrDefault(m => m.Id == messageId);

            if (message == null) return null;
            if (!CanSeeMessage(token, message)) return null;

            return message;
        }

        public IMessage CreateMessage(IToken token, string receiver, string content)
        {
            var iReceiver = Connection.UserService.GetUser(receiver);
            return CreateMessage(token, iReceiver, content);
        }

        public IMessage CreateMessage(IToken token, IUser receiver, string content)
        {
            if (!CanSendMessage(token, receiver)) return null;

            var msg = new Message
            {
                Sender = Connection.UserService.GetUser(token.User),
                Receiver = Connection.UserService.GetUser(receiver),
                Content = content,
                Sent = DateTime.UtcNow
            };
            Connection.Context.Messages.Add(msg);
            Connection.MessageListenerService.QueueMessage(msg);
            return msg;
        }

        public IEnumerable<IMessage> GetMessages(
            IToken token,
            bool includeSent,
            bool includeReceived,
            string sender = null,
            int? afterId = null,
            DateTime? afterTime = null)
        {
            IQueryable<Message> result = Connection.Context.Messages;

            if (includeReceived && includeSent)
            {
                result = result.Where(m => m.Receiver.Username == token.User.Username ||
                                           m.Sender.Username == token.User.Username);
            }
            else if (includeSent)
            {
                result = result.Where(m => m.Sender.Username == token.User.Username);
            }
            else if (includeReceived)
            {
                result = result.Where(m => m.Receiver.Username == token.User.Username);
            }
            else return Enumerable.Empty<IMessage>();

            if (sender != null)
            {
                result = result.Where(m => m.Sender.Username == sender ||
                                           m.Receiver.Username == sender);
            }
            if (afterId != null)
            {
                result = result.Where(m => m.Id > afterId);
            }
            if (afterTime != null)
            {
                result = result.Where(m => m.Sent > afterTime);
            }

            return result.OrderBy(m => m.Sent);
        }

        public bool CanSendMessage(IToken token, IUser receiver)
        {
            return token.User.Friends.Contains(receiver) ||
                token.User.FriendRequests.Contains(receiver) ||
                token.User == receiver;
        }

        public bool CanSeeMessage(IToken token, IMessage message)
        {
            return token.User == message.Sender ||
                   token.User == message.Receiver;
        }

    }
}

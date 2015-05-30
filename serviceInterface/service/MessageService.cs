using System;
using System.Collections.Generic;
using System.Linq;
using backend.model;
using modelInterface;

namespace serviceInterface.service
{
    public class MessageService
    {
        private readonly Connection _conn;

        internal MessageService(Connection conn)
        {
            _conn = conn;
        }

        public IMessage GetMessage(IToken token, int messageId)
        {
            var message = _conn.Context.Messages.FirstOrDefault(m => m.Id == messageId);

            if (message == null) return null;
            if (!CanSeeMessage(token, message)) return null;

            return message;
        }

        public IMessage CreateMessage(IToken token, string receiver, string content)
        {
            var iReceiver = _conn.UserService.GetUser(receiver);
            return CreateMessage(token, iReceiver, content);
        }

        public IMessage CreateMessage(IToken token, IUser receiver, string content)
        {
            if (!CanSendMessage(token, receiver)) return null;

            var msg = new Message
            {
                Sender = _conn.UserService.GetUser(token.User),
                Receiver = _conn.UserService.GetUser(receiver),
                Content = content,
                Sent = DateTime.UtcNow
            };
            _conn.Context.Messages.Add(msg);
            return msg;
        }

        public IEnumerable<IMessage> GetMessages(IUser receiver, IUser sender = null, DateTime? filterDate = null)
        {
            IQueryable<Message> result = _conn.Context.Messages;
            result = result.Where(m => m.Receiver.Username == receiver.Username);
            if (sender != null)
            {
                result = result.Where(m => m.Sender.Username == sender.Username);
            }
            if (filterDate != null)
            {
                result = result.Where(m => m.Sent >= filterDate);
            }
            return result;
        }

        public bool CanSendMessage(IToken token, IUser receiver)
        {
            return token.User.Friends.Contains(receiver) ||
                token.User == receiver;
        }

        public bool CanSeeMessage(IToken token, IMessage message)
        {
            return token.User == message.Sender ||
                   token.User == message.Receiver;
        }

    }
}

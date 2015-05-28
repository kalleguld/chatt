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

        public IMessage CreateMessage(IToken token, IUser receiver, string content)
        {
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
            result = result.Where(m=> m.Receiver.Username == receiver.Username);
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
    }
}

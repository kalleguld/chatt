using System;
using System.Collections.Generic;
using System.Linq;
using backend;
using backend.model;
using modelInterface;

namespace serviceInterface.service
{
   public class MessageService
    {
        private readonly Context _context;

        public MessageService(Context context)
        {
            _context = context;
        }

        public IMessage CreateMessage(IToken token, IUser receiver, string content)
        {
            var msg = new Message
            {
                Sender = (User) token.User, 
                Receiver = (User)receiver, 
                Content = content, 
                Sent = DateTime.UtcNow
            };
            _context.Messages.Add(msg);
            return msg;
        }

        public IEnumerable<IMessage> GetMessages(IUser receiver, IUser sender = null, DateTime? filterDate = null)
        {
            IQueryable<Message> result = _context.Messages;
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

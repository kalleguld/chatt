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

        public IMessage CreateMessage(IUser sender, IUser receiver, string content)
        {
            var msg = new Message
            {
                Sender = (User) sender, 
                Receiver = (User)receiver, 
                Content = content, 
                Sent = DateTime.UtcNow
            };
            _context.Messages.Add(msg);
            return msg;
        }

        public IEnumerable<IMessage> GetMessages(IUser receiver, IUser sender = null, DateTime? filterDate = null)
        {
            var result = _context.Messages.Where(m=> m.Receiver == receiver);
            if (sender != null)
            {
                result = result.Where(m => m.Sender == sender);
            }
            if (filterDate != null)
            {
                result = result.Where(m => m.Sent >= filterDate);
            }
            return result;
        }
    }
}

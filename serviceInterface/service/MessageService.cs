using System;
using System.Collections.Generic;
using System.Linq;
using backend;
using backend.model;
using modelInterface;
using modelInterface.exceptions;

namespace serviceInterface.service
{
    public class MessageService : BaseService
    {
        private readonly Context _context;
        private readonly UserService _userService;

        internal MessageService(Context context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IMessage GetMessage(IToken token, int messageId)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);

            if (message == null) return null;
            if (!CanSeeMessage(token, message)) return null;

            return message;
        }

        public IMessage CreateMessage(IToken token, string receiver, string content)
        {
            var iReceiver = _userService.GetUser(receiver);
            return CreateMessage(token, iReceiver, content);
        }

        public IMessage CreateMessage(IToken token, IUser receiver, string content)
        {
            if (!CanSendMessage(token, receiver)) throw new InsufficientRights();
            if (string.IsNullOrWhiteSpace(content)) throw new MessageContentEmpty();

            var msg = new Message
            {
                Sender = _userService.GetUser(token.User),
                Receiver = _userService.GetUser(receiver),
                Content = content,
                Sent = DateTime.UtcNow
            };
            _context.Messages.Add(msg);
            return msg;
        }

        public IEnumerable<IMessage> GetMessages(
            IToken token,
            bool includeSent,
            bool includeReceived,
            string sender = null,
            int? afterId = null,
            DateTime? afterTime = null,
            int? maxResults = null)
        {
            IQueryable<Message> result = _context.Messages;

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
            if (maxResults != null && maxResults > 0)
            {
                result = result.OrderByDescending(m => m.Sent).Take(maxResults??0);
            }

            return result.OrderBy(m => m.Sent);
        }

        public IEnumerable<IMessage> GetMessages(int afterId)
        {
            return _context.Messages.Where(m => m.Id > afterId);
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

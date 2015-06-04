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
        public delegate void MessageCreated(IMessage message);

        private static MessageCreated _messageCreatedDel;
        private readonly Queue<IMessage> _pendingMessages = new Queue<IMessage>();

        public void AddMessageCreatedDelegate(MessageCreated del)
        {
            _messageCreatedDel += del;
        }

        public void RemoveMessageCreatedDelegate(MessageCreated del)
        {
            // ReSharper disable once DelegateSubtraction
            _messageCreatedDel -= del;
        }

        internal void QueueMessage(IMessage message)
        {
            _pendingMessages.Enqueue(message);
        }

        internal void DatabaseSaved()
        {
            foreach (var message in _pendingMessages)
            {
                if (_messageCreatedDel != null) _messageCreatedDel(message);
            }
        }
    }

}

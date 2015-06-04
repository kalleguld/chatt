using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using serviceInterface;
using serviceInterface.service;
using WcfWsComms;

namespace rerest.controllers
{
    public class NewMessageController : INewMessageServer
    {
        private readonly ConnectionFactory _connectionFactory = new ConnectionFactory();

        public async Task SendMessages()
        {
            var callback = OperationContext.Current.GetCallbackChannel<INewMessageClient>();
            var callbackChannel = (IChannel)callback;
            var pendingMessages = new BufferBlock<MessageInfo>();

            MessageListenerService.MessageCreated mcDel = (iMessage) =>
                pendingMessages.Post(new MessageInfo
                {
                    MessageId = iMessage.Id,
                    Sender = iMessage.Sender.Username,
                    Receiver = iMessage.Receiver.Username
                });

            using (var conn = _connectionFactory.GetConnection())
            {
                conn.MessageListenerService.AddMessageCreatedDelegate(mcDel);
            }

            try
            {
                var message = await pendingMessages.ReceiveAsync();
                while (callbackChannel.State == CommunicationState.Opened)
                {
                    await callback.NewMessageCreated(message);
                    message = await pendingMessages.ReceiveAsync();
                }
            }
            finally
            {
                using (var conn = _connectionFactory.GetConnection())
                {
                    conn.MessageListenerService.RemoveMessageCreatedDelegate(mcDel);
                }
            }
        }
    }
}

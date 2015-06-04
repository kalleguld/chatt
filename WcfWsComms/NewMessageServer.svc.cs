using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace WcfWsComms
{
    
    public class NewMessageServer : INewMessageServer
    {
        public async Task SendMessages()
        {
            var callback = OperationContext.Current.GetCallbackChannel<INewMessageClient>();

            MessageInfo message = null;

            while (((IChannel)callback).State == CommunicationState.Opened)
            {
                await callback.NewMessageCreated(new MessageInfo
                {
                    MessageId = 4, 
                    Receiver = "test", 
                    Sender = "senderTest"
                });
                await Task.Delay(1000);
                //if (message != null)
                //{
                //    await callback.NewMessageCreated(message);
                //}

                //lock (pendingMessageIds)
                //{
                //    if (pendingMessageIds.Count == 0)
                //    {
                //        message = null;
                //        Monitor.Wait(pendingMessageIds);
                //    }
                //    else
                //    {
                //        message = pendingMessageIds.Dequeue();
                //    }
                //}
            }
        }
    }
}

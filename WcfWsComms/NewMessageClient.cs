using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;

namespace WcfWsComms
{
    public class NewMessageClient : DuplexClientBase<INewMessageClient>, INewMessageClient
    {
        public delegate void NewMessage(MessageInfo mi);

        private NewMessage _messageDelegate;

        public NewMessageClient(InstanceContext callbackInstance, NewMessage messageDelegate) : 
            base(callbackInstance)
        {
            _messageDelegate = messageDelegate;
        }

        public NewMessageClient(InstanceContext callbackInstance, 
            string endpointConfigurationName, 
            NewMessage messageDelegate) : 
                base(callbackInstance, endpointConfigurationName)
        {
            _messageDelegate = messageDelegate;
        }

        public Task NewMessageCreated(MessageInfo message)
        {
            _messageDelegate(message);
            return null;
        }
    }
}
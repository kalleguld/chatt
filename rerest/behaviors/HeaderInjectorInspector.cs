using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace rerest.behaviors
{
    public class HeaderInjectorInspector : IDispatchMessageInspector
    {
        private readonly IDictionary<string, string> _headers;

        public HeaderInjectorInspector(IDictionary<string, string> headers)
        {
            _headers = headers;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var httpHeader = (HttpResponseMessageProperty)reply.Properties["httpResponse"];
            foreach (var header in _headers)
            {
                httpHeader.Headers.Add(header.Key, header.Value);
            }
        }
    }
}

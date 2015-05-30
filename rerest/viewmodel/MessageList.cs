using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class MessageList : JsonResponse
    {
        [DataMember]
        public IList<int> Messages { get; set; }

        public MessageList() { Messages = new List<int>(); }

        public MessageList(IEnumerable<IMessage> messages) : this()
        {
            Messages = messages.Select(m => m.Id).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using modelInterface;
using rerest.jsonBase;
using utils;

namespace rerest.viewmodel
{
    [DataContract]
    public class MessageInfo : JsonResponse
    {
        [DataMember(Name = "id")]
        public int Id { get; private set; }
        [DataMember(Name = "receiver")]
        public string Receiver { get; private set; }
        [DataMember(Name = "sender")]
        public string Sender { get; private set; }
        [DataMember(Name = "sent")]
        public long Sent { get; private set; }
        [DataMember(Name = "content")]
        public string Content { get; private set; }
        [DataMember(Name = "outgoing")]
        public bool Outgoing { get; private set; }

        public MessageInfo() { }

        public MessageInfo(IMessage message, bool outgoing)
        {
            Id = message.Id;
            Receiver = message.Receiver.Username;
            Sender = message.Sender.Username;
            Sent = message.Sent.ToMilis();
            Content = message.Content;
            Outgoing = outgoing;
        }
    }
}

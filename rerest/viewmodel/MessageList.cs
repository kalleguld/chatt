using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;
using utils;

namespace rerest.viewmodel
{
    [DataContract]
    public class MessageList : JsonResponse
    {
        [DataMember(Name = "messages")]
        public IList<LightMessageInfo> Messages { get; private set; }

        public MessageList() { Messages = new List<LightMessageInfo>(); }

        public MessageList(IEnumerable<IMessage> messages, string sourceUsername) : this()
        {
            Messages = messages.Select(m => new LightMessageInfo(m, sourceUsername)).ToList();
        }
    }

    [DataContract]
    public class LightMessageInfo
    {
        [DataMember(Name = "contents")]
        public string Contents { get; set; }

        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name = "outgoing")]
        public bool Outgoing { get; set; }

        [DataMember(Name="sender")]
        public string Sender { get; set; }

        [DataMember(Name="receiver")]
        public string Receiver { get; set; }

        [DataMember(Name = "sent")]
        public long Sent { get; set; }

        public LightMessageInfo() { }

        public LightMessageInfo(IMessage message, string sourceUsername)
        {
            Contents = message.Content;
            Id = message.Id;
            Outgoing = (sourceUsername == message.Sender.Username);
            Sender = message.Sender.Username;
            Receiver = message.Receiver.Username;
            Sent = message.Sent.ToMilis();
        }
    }
}

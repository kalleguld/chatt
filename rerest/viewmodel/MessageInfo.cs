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
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Receiver { get; set; }
        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public long Sent { get; set; }
        [DataMember]
        public string Content { get; set; }


        public MessageInfo() { }

        public MessageInfo(IMessage message)
        {
            Id = message.Id;
            Receiver = message.Receiver.Username;
            Sender = message.Sender.Username;
            Sent = message.Sent.ToMilis();
            Content = message.Content;
        }
    }
}

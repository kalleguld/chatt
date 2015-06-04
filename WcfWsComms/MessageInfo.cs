using System.Runtime.Serialization;

namespace WcfWsComms
{
    [DataContract]
    public class MessageInfo
    {
        [DataMember]
        public int MessageId { get; set; }
        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public string Receiver { get; set; }

    }
}
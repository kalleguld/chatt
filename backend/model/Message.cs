using System;
using System.ComponentModel.DataAnnotations;
using modelInterface;

namespace backend.model
{
    public class Message : IMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Sent { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }

        IUser IMessage.Sender { get { return Sender;} }
        IUser IMessage.Receiver { get { return Receiver; } }
    }
}

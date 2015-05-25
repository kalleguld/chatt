using System;
using System.ComponentModel.DataAnnotations;

namespace model.model
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Sent { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}

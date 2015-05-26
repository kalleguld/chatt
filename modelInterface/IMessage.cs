using System;

namespace modelInterface
{
    public interface IMessage
    {
        int Id { get; }
        string Content { get; }
        DateTime Sent { get; }
        IUser Sender { get; }
        IUser Receiver { get; }
    }
}

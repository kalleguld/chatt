using System;

namespace modelInterface
{
    public interface IToken
    {
        Guid Guid { get; }
        IUser User { get; }
    }
}

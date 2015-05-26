using System.Collections.Generic;

namespace modelInterface
{
    public interface IUser
    {
        string Username { get; }
        string FullName { get; }
        IEnumerable<IUser> Friends { get; }
        IEnumerable<IUser> FriendRequests { get; }
    }
}

using System.Collections.Generic;
using backend;
using backend.model;
using modelInterface;

namespace serviceInterface.service
{
    public class FriendService
    {
        private readonly Context _context;

        public FriendService(Context context)
        {
            _context = context;
        }

        public IEnumerable<IUser> GetFriends(IUser user)
        {
            return user.Friends;
        } 

        public void AddFriend(IToken token, IUser friend)
        {
            ((Token)token).User.Friends.Add((User)friend);
        }

        public void RemoveFriend(IToken token, IUser friend)
        {
            ((Token)token).User.Friends.Remove((User)friend);
        }
    }
}

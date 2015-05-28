using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using modelInterface;

namespace backend.model
{
    public class User : IUser
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Hash { get; set; }

        public virtual ISet<User> Friends
        {
            get {return _friends;}
            set { _friends = value; }
        }

        public virtual ISet<User> FriendRequests
        {
            get { return _friendRequests; }
            set { _friendRequests = value; }
        }

        public virtual ISet<User> RequestedFriends
        {
            get { return _requestedFriends; }
            set { _requestedFriends = value; }
        }

        public virtual ISet<User> ReverseFriends
        {
            get { return _reverseFriends; }
            set { _reverseFriends = value; }
        }


        private ISet<User> _friendRequests;
        private ISet<User> _friends;
        private ISet<User> _requestedFriends;
        private ISet<User> _reverseFriends;

        IEnumerable<IUser> IUser.Friends { get { return Friends; } }
        IEnumerable<IUser> IUser.FriendRequests { get { return FriendRequests; } } 


        public User()
        {
            _friends = new HashSet<User>();
            _friendRequests = new HashSet<User>();
            _requestedFriends = new HashSet<User>();
            _reverseFriends = new HashSet<User>();
        }
    }
}


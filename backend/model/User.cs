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

        public virtual ISet<Message> SentMessages
        {
            get { return _sentMessages; }
            set { _sentMessages = value; }
        }

        public virtual ISet<Message> ReceivedMessages
        {
            get { return _receivedMessages; }
            set { _receivedMessages = value; }
        }

        private ISet<User> _friends;
        private ISet<User> _friendRequests;
        private ISet<User> _requestedFriends;
        private ISet<Message> _sentMessages;
        private ISet<Message> _receivedMessages;

        IEnumerable<IUser> IUser.Friends { get { return Friends; } }
        IEnumerable<IUser> IUser.FriendRequests { get { return FriendRequests; } } 


        public User()
        {
            _friends = new HashSet<User>();
            _friendRequests = new HashSet<User>();
            _requestedFriends = new HashSet<User>();
            _sentMessages = new HashSet<Message>();
            _receivedMessages = new HashSet<Message>();
        }
    }
}


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using modelInterface;

namespace backend.model
{
    public class User : IUser
    {
        [Key]
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Hash { get; set; }
        public virtual ISet<User> Friends { get {return _friends;} set { _friends = value; } }
        public virtual ISet<User> FriendRequests { get { return _friendRequests; } set { _friendRequests = value; } }


        private ISet<User> _friendRequests;
        private ISet<User> _friends;

        IEnumerable<IUser> IUser.Friends { get { return Friends; } }
        IEnumerable<IUser> IUser.FriendRequests { get { return FriendRequests; } } 


        public User()
        {
            _friends = new HashSet<User>();
            _friendRequests = new HashSet<User>();
        }
    }
}


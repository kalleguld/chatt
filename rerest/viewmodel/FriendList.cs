using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class FriendList : JsonResponse
    {
        [DataMember(Name = "friends")]
        public IList<string> Friends { get; private set; }

        [DataMember(Name = "friendRequests")]
        public IList<string> FriendRequests { get; private set; }

        public FriendList()
        {
            Friends = new List<string>();
            FriendRequests = new List<string>();
        }

        public FriendList(IUser user)
        {
            Friends = user.Friends.Select(u => u.Username).ToList();
            FriendRequests = user.FriendRequests.Select(u => u.Username).ToList();
        }
    }
}

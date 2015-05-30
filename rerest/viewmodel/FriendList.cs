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
        [DataMember]
        IList<string> Friends { get; set; }

        [DataMember]
        IList<string> FriendRequests { get; set; }

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

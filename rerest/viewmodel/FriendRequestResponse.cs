using System.Runtime.Serialization;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class FriendRequestResponse : JsonResponse
    {
        [DataMember(Name = "friendAdded")]
        public bool FriendAddded { get; private set; }

        public FriendRequestResponse(bool friendAdded = false)
        {
            FriendAddded = friendAdded;
        }
    }
}

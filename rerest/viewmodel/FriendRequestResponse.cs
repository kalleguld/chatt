using System.Runtime.Serialization;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class FriendRequestResponse : JsonResponse
    {
        [DataMember]
        public bool FriendAddded { get; set; }

        public FriendRequestResponse() { }
        
        public FriendRequestResponse(bool friendAdded) : this()
        {
            FriendAddded = friendAdded;
        }

        
    }
}

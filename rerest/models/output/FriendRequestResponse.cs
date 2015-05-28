using System.Runtime.Serialization;

namespace rerest.models.output
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

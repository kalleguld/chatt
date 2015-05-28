using System.Runtime.Serialization;
using modelInterface;

namespace rerest.models.output
{
    [DataContract]
    public class UserInfo : JsonResponse
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Username { get; set; }
        
        public UserInfo() { }
        public UserInfo(IUser user)
        {
            FullName = user.FullName;
            Username = user.Username;
        }
        
    }
}

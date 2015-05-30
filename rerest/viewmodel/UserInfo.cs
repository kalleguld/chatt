using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class UserInfo : JsonResponse
    {
        [DataMember]
        public string FullName { get; private set; }
        [DataMember]
        public string Username { get; private set; }
        
        public UserInfo() { }
        public UserInfo(IUser user)
        {
            FullName = user.FullName;
            Username = user.Username;
        }
        
    }
}

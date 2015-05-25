using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using backend.model;
using rerest.models.@base;

namespace rerest.models.output
{
    [DataContract]
    public class UserInfo : JsonResponse
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public IList<UserBasic> Friends { get; set; }
        
        public UserInfo() { }
        public UserInfo(User user)
        {
            FullName = user.FullName;
            Username = user.Username;
            Friends = user.Friends
                .Select(u => new UserBasic(u))
                .ToList();
        }
        
    }
}

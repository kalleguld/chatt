using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class UserList : JsonResponse
    {
        [DataMember(Name = "users")]
        public IList<LightUserInfo> Users { get; set; }

        public UserList() { }

        public UserList(IEnumerable<IUser> users)
        {
            Users = users.Select(u => new LightUserInfo(u)).ToList();
        }
    }

    [DataContract]
    public class LightUserInfo
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        public LightUserInfo() { }

        public LightUserInfo(IUser user)
        {
            Username = user.Username;
            FullName = user.FullName;
        }
    }
}

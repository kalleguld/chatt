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
        [DataMember(Name = "usernames")]
        public IList<string> Usernames { get; set; }

        public UserList() { }

        public UserList(IEnumerable<IUser> users)
        {
            Usernames = users.Select(u=>u.Username).ToList();
        }
    }
}

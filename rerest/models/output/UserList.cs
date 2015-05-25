using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using backend.model;
using rerest.models.@base;

namespace rerest.models.output
{
    [DataContract]
    public class UserList : JsonResponse
    {
        [DataMember]
        IList<string> Friends { get; set; }

        public UserList() { Friends = new List<string>();}

        public UserList(IEnumerable<User> users)
        {
            Friends = users.Select(u => u.Username).ToList();
        }
    }
}

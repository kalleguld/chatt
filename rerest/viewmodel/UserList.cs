﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class UserList : JsonResponse
    {
        [DataMember]
        public IList<string> Users { get; set; }

        public UserList() { }

        public UserList(IEnumerable<IUser> users)
        {
            Users = users.Select(u=>u.Username).ToList();
        }
    }
}

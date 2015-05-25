using System.Runtime.Serialization;
using backend.model;

namespace rerest.models.@base
{

    [DataContract]
    public class UserBasic
    {
        [DataMember]
        public string Username { get; set; }

        public UserBasic() { }
        public UserBasic(User user) : this()
        {
            Username = user.Username;
        }
    }
}

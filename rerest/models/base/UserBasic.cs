using System.Runtime.Serialization;
using modelInterface;

namespace rerest.models.@base
{

    [DataContract]
    public class UserBasic
    {
        [DataMember]
        public string Username { get; set; }

        public UserBasic() { }
        public UserBasic(IUser user) : this()
        {
            Username = user.Username;
        }
    }
}

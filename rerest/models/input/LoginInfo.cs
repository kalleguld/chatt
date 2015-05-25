using System.Runtime.Serialization;

namespace rerest.models.input
{
    [DataContract]
    public class LoginInfo
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}

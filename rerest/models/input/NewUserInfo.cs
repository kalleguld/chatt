using System.Runtime.Serialization;

namespace rerest.models.input
{
    [DataContract]
    public class NewUserInfo
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}

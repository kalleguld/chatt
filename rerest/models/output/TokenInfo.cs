using System;
using System.Runtime.Serialization;
using modelInterface;

namespace rerest.models.output
{
    [DataContract]
    public class TokenInfo : JsonResponse
    {
        [DataMember]
        public Guid Token { get; set; }
        [DataMember]
        public string Username { get; set; }

        public TokenInfo() { }

        public TokenInfo(IToken token) : this()
        {
            Token = token.Guid;
            Username = token.User.Username;
        }
    }
}

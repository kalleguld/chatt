using System;
using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    public class TokenInfo : JsonResponse
    {
        [DataMember(Name = "token")]
        public Guid Token { get; private set; }
        [DataMember(Name = "username")]
        public string Username { get; private set; }

        public TokenInfo() { }

        public TokenInfo(IToken token) : this()
        {
            Token = token.Guid;
            Username = token.User.Username;
        }
    }
}

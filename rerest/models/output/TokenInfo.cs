using System;
using modelInterface;
using rerest.models.@base;

namespace rerest.models.output
{
    public class TokenInfo : JsonResponse
    {
        public Guid Guid { get; set; }
        public UserBasic User { get; set; }

        public TokenInfo() { }
        public TokenInfo(IToken token)
        {
            Guid = token.Guid;
            User = new UserBasic(token.User);
        }
    }
}

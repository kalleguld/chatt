using System;
using backend.model;
using rerest.models.@base;

namespace rerest.models.output
{
    public class TokenInfo : JsonResponse
    {
        public Guid Guid { get; set; }
        public UserBasic User { get; set; }

        public TokenInfo() { }
        public TokenInfo(Token token)
        {
            Guid = token.Guid;
            User = new UserBasic(token.User);
        }
    }
}

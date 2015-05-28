using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using modelInterface;

namespace rerest.models.output
{
    [DataContract]
    class TokenList : JsonResponse
    {
        [DataMember]
        public IList<Guid> Tokens { get; set; }

        public TokenList() { Tokens = new List<Guid>(); }

        public TokenList(IEnumerable<IToken> tokens)
        {
            Tokens = tokens.Select(t => t.Guid).ToList();
        }
    }
}

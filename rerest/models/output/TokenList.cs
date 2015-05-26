using System;
using System.Collections.Generic;
using System.Linq;
using modelInterface;

namespace rerest.models.output
{
    class TokenList : JsonResponse
    {
        public IList<Guid> Tokens { get; set; }
        public TokenList() { Tokens = new List<Guid>(); }

        public TokenList(IEnumerable<IToken> tokens)
        {
            Tokens = tokens.Select(t => t.Guid).ToList();
        }
    }
}

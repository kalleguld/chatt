﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using modelInterface;
using rerest.jsonBase;

namespace rerest.viewmodel
{
    [DataContract]
    class TokenList : JsonResponse
    {
        [DataMember(Name = "tokens")]
        public IList<Guid> Tokens { get; private set; }

        public TokenList() { Tokens = new List<Guid>(); }

        public TokenList(IEnumerable<IToken> tokens)
        {
            Tokens = tokens.Select(t => t.Guid).ToList();
        }
    }
}

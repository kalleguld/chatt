using System;
using System.ComponentModel.DataAnnotations;
using modelInterface;

namespace backend.model
{
    public class Token : IToken
    {
        public Guid Guid { get; set; }
        public virtual User User { get; set; }

        IUser IToken.User { get { return User; } }
    }
}

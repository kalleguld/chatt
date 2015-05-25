using System;
using System.ComponentModel.DataAnnotations;

namespace model.model
{
    public class Token
    {
        [Key]
        public Guid Guid { get; set; }
        public virtual User User { get; set; }
    }
}

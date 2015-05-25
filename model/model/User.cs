using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model.model
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Hash { get; set; }
        public virtual ISet<User> Friends { get; set; }

        public User()
        {
            Friends = new HashSet<User>();
        }
    }
}


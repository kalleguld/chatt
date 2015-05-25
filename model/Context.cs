using System.Data.Entity;
using model.model;

namespace model
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Token> Tokens { get; set; } 
    }
}

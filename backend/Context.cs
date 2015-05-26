using System.Data.Entity;
using backend.model;

namespace backend
{
    public class Context : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; } 
    }
}

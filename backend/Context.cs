using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using backend.model;

namespace backend
{
    public class Context : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbmb)
        {

            var user = dbmb.Entity<User>();
            user.ToTable("user");
            user.HasKey(u => u.Username);
            user.Property(u => u.FullName).IsRequired();
            user.Property(u => u.Hash).IsRequired();

            user.HasMany(u => u.Friends)
                .WithMany(u => u.ReverseFriends)
                .Map(ff =>
                {
                    ff.MapLeftKey("user");
                    ff.MapRightKey("friend");
                    ff.ToTable("userFriend");
                });

            user.HasMany(u => u.FriendRequests)
                .WithMany(u => u.RequestedFriends)
                .Map(fr =>
                {
                    fr.MapLeftKey("user");
                    fr.MapRightKey("request");
                    fr.ToTable("userFriendRequest");
                });



            var token = dbmb.Entity<Token>();
            token.ToTable("token");
            token.HasKey(t => t.Guid);
            token.HasRequired(t => t.User).WithMany();


            var message = dbmb.Entity<Message>();
            message.ToTable("message");
            message.HasKey(m => m.Id);
            message.Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            message.Property(m => m.Sent).IsRequired();
            message.Property(m => m.Content).IsRequired();
            message.HasOptional(m => m.Sender).WithMany(u=>u.SentMessages);
            message.HasRequired(m => m.Receiver).WithMany(u=>u.ReceivedMessages);

            base.OnModelCreating(dbmb);
        }
    }
}

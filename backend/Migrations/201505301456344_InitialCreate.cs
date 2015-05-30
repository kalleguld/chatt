namespace backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Sent = c.DateTime(nullable: false),
                        Receiver_Username = c.String(nullable: false, maxLength: 128),
                        Sender_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.user", t => t.Receiver_Username, cascadeDelete: true)
                .ForeignKey("dbo.user", t => t.Sender_Username)
                .Index(t => t.Receiver_Username)
                .Index(t => t.Sender_Username);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false),
                        Hash = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.token",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        User_Username = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.user", t => t.User_Username, cascadeDelete: true)
                .Index(t => t.User_Username);
            
            CreateTable(
                "dbo.userFriendRequest",
                c => new
                    {
                        user = c.String(nullable: false, maxLength: 128),
                        request = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.user, t.request })
                .ForeignKey("dbo.user", t => t.user)
                .ForeignKey("dbo.user", t => t.request)
                .Index(t => t.user)
                .Index(t => t.request);
            
            CreateTable(
                "dbo.userFriend",
                c => new
                    {
                        user = c.String(nullable: false, maxLength: 128),
                        friend = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.user, t.friend })
                .ForeignKey("dbo.user", t => t.user)
                .ForeignKey("dbo.user", t => t.friend)
                .Index(t => t.user)
                .Index(t => t.friend);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.token", "User_Username", "dbo.user");
            DropForeignKey("dbo.message", "Sender_Username", "dbo.user");
            DropForeignKey("dbo.message", "Receiver_Username", "dbo.user");
            DropForeignKey("dbo.userFriend", "friend", "dbo.user");
            DropForeignKey("dbo.userFriend", "user", "dbo.user");
            DropForeignKey("dbo.userFriendRequest", "request", "dbo.user");
            DropForeignKey("dbo.userFriendRequest", "user", "dbo.user");
            DropIndex("dbo.userFriend", new[] { "friend" });
            DropIndex("dbo.userFriend", new[] { "user" });
            DropIndex("dbo.userFriendRequest", new[] { "request" });
            DropIndex("dbo.userFriendRequest", new[] { "user" });
            DropIndex("dbo.token", new[] { "User_Username" });
            DropIndex("dbo.message", new[] { "Sender_Username" });
            DropIndex("dbo.message", new[] { "Receiver_Username" });
            DropTable("dbo.userFriend");
            DropTable("dbo.userFriendRequest");
            DropTable("dbo.token");
            DropTable("dbo.user");
            DropTable("dbo.message");
        }
    }
}

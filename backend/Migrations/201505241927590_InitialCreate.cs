namespace backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Sent = c.DateTime(nullable: false),
                        Receiver_Username = c.String(maxLength: 128),
                        Sender_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Receiver_Username)
                .ForeignKey("dbo.Users", t => t.Sender_Username)
                .Index(t => t.Receiver_Username)
                .Index(t => t.Sender_Username);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Hash = c.String(),
                        User_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Users", t => t.User_Username)
                .Index(t => t.User_Username);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        User_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.User_Username)
                .Index(t => t.User_Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "User_Username", "dbo.Users");
            DropForeignKey("dbo.Messages", "Sender_Username", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_Username", "dbo.Users");
            DropForeignKey("dbo.Users", "User_Username", "dbo.Users");
            DropIndex("dbo.Tokens", new[] { "User_Username" });
            DropIndex("dbo.Users", new[] { "User_Username" });
            DropIndex("dbo.Messages", new[] { "Sender_Username" });
            DropIndex("dbo.Messages", new[] { "Receiver_Username" });
            DropTable("dbo.Tokens");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}

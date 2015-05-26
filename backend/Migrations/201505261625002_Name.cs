namespace backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "User_Username", "dbo.Users");
            DropIndex("dbo.Users", new[] { "User_Username" });
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_Username = c.String(nullable: false, maxLength: 128),
                        User_Username1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_Username, t.User_Username1 })
                .ForeignKey("dbo.Users", t => t.User_Username)
                .ForeignKey("dbo.Users", t => t.User_Username1)
                .Index(t => t.User_Username)
                .Index(t => t.User_Username1);
            
            DropColumn("dbo.Users", "User_Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "User_Username", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserUsers", "User_Username1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Username", "dbo.Users");
            DropIndex("dbo.UserUsers", new[] { "User_Username1" });
            DropIndex("dbo.UserUsers", new[] { "User_Username" });
            DropTable("dbo.UserUsers");
            CreateIndex("dbo.Users", "User_Username");
            AddForeignKey("dbo.Users", "User_Username", "dbo.Users", "Username");
        }
    }
}

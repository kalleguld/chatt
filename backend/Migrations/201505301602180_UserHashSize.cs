namespace backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserHashSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.user", "Hash", c => c.String(nullable: false, maxLength: 60, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.user", "Hash", c => c.String(nullable: false));
        }
    }
}

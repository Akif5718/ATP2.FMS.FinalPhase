namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavedFiles", "PostId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavedFiles", "PostId");
        }
    }
}

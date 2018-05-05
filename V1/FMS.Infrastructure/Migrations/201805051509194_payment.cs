namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PostId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "PostId");
        }
    }
}

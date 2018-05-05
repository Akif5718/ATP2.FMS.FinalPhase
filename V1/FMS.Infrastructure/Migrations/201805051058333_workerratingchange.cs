namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workerratingchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RatingWorkers", "PostId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RatingWorkers", "PostId");
        }
    }
}

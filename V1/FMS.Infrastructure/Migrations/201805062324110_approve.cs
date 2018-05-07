namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class approve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedWorkers", "Approved", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SelectedWorkers", "Approved");
        }
    }
}

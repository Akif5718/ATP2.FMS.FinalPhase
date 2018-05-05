namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class selectedworker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedWorkers", "Flag", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SelectedWorkers", "Flag");
        }
    }
}

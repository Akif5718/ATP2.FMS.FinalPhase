namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_flag_to_responsetoApro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResponseToaJobs", "Flag", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResponseToaJobs", "Flag");
        }
    }
}

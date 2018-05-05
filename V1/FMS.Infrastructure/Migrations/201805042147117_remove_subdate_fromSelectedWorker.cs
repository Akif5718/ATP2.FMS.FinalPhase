namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_subdate_fromSelectedWorker : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SelectedWorkers", "SubmissionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SelectedWorkers", "SubmissionDate", c => c.DateTime(nullable: false));
        }
    }
}

namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkerInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkerInfoes",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        EarnedMoney = c.Single(nullable: false),
                        RatePerHour = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkerInfoes");
        }
    }
}

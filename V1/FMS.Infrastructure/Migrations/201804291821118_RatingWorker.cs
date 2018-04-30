namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingWorker : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingWorkers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        CommunicationSkill = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        OnBudget = c.Int(nullable: false),
                        Behaviour = c.Int(nullable: false),
                        Completeness = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RatingWorkers");
        }
    }
}

namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkHistories",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        Experience = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkHistories");
        }
    }
}

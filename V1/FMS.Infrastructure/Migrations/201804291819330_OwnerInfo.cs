namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OwnerInfoes",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        CompanyAddress = c.String(nullable: false),
                        CompanyCode = c.String(nullable: false),
                        Position = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OwnerInfoes");
        }
    }
}

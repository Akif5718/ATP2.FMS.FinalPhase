namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AverageRatingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AverageRatings",
                c => new
                    {
                        AverageRatingId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Average = c.Double(nullable: false),
                        UserType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AverageRatingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AverageRatings");
        }
    }
}

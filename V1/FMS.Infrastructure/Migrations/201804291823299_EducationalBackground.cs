namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EducationalBackground : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducationalBackgrounds",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        School = c.String(),
                        Collage = c.String(),
                        UniversityPost = c.String(),
                        UniversityUnder = c.String(),
                        Others = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EducationalBackgrounds");
        }
    }
}

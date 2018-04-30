namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostAProjects",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        WUserId = c.Int(nullable: false),
                        ProjectName = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Members = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.ProjectSections",
                c => new
                    {
                        ProjectSectionId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        SectionName = c.String(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectSectionId);
            
            CreateTable(
                "dbo.ProjectSkills",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.ResponseToaJobs",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        WUserId = c.Int(nullable: false),
                        FixedPrice = c.Single(nullable: false),
                        SubmissionTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.SelectedWorkers",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SelectedWorkers");
            DropTable("dbo.ResponseToaJobs");
            DropTable("dbo.ProjectSkills");
            DropTable("dbo.ProjectSections");
            DropTable("dbo.PostAProjects");
        }
    }
}

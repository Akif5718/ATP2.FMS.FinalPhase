namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.COMMENTSECs",
                c => new
                    {
                        CommunicationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectSectionId = c.Int(nullable: false),
                        Commt = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommunicationId);
            
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
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OUserId = c.Int(nullable: false),
                        WUserId = c.Int(nullable: false),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.RatingOwners",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        CommunicationSkill = c.Int(nullable: false),
                        Reliability = c.Int(nullable: false),
                        OnWord = c.Int(nullable: false),
                        Behaviour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
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
                "dbo.SavedFiles",
                c => new
                    {
                        SavedFileId = c.Int(nullable: false, identity: true),
                        ProjectSectionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        FileLink = c.String(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SavedFileId);
            
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
            
            CreateTable(
                "dbo.SkillCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SkillId);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FristName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        DateofBrith = c.DateTime(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        ProPic = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        UserType = c.String(nullable: false),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.WorkerInfoes",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        EarnedMoney = c.Single(nullable: false),
                        RatePerHour = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.WorkerSkills",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.WorkReports",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkReports");
            DropTable("dbo.WorkHistories");
            DropTable("dbo.WorkerSkills");
            DropTable("dbo.WorkerInfoes");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillCategories");
            DropTable("dbo.SelectedWorkers");
            DropTable("dbo.SavedFiles");
            DropTable("dbo.ResponseToaJobs");
            DropTable("dbo.RatingWorkers");
            DropTable("dbo.RatingOwners");
            DropTable("dbo.ProjectSkills");
            DropTable("dbo.ProjectSections");
            DropTable("dbo.PostAProjects");
            DropTable("dbo.Payments");
            DropTable("dbo.OwnerInfoes");
            DropTable("dbo.EducationalBackgrounds");
            DropTable("dbo.COMMENTSECs");
        }
    }
}

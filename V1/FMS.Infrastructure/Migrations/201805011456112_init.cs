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
                        EducationalBackgroundId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        School = c.String(),
                        Collage = c.String(),
                        UniversityPost = c.String(),
                        UniversityUnder = c.String(),
                        Others = c.String(),
                    })
                .PrimaryKey(t => t.EducationalBackgroundId);
            
            CreateTable(
                "dbo.OwnerInfoes",
                c => new
                    {
                        OwnerInfoId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false),
                        CompanyAddress = c.String(nullable: false),
                        CompanyCode = c.String(nullable: false),
                        Position = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OwnerInfoId);
            
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
                        ProjectSkillId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectSkillId);
            
            CreateTable(
                "dbo.RatingOwners",
                c => new
                    {
                        RatingOwnerId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CommunicationSkill = c.Int(nullable: false),
                        Reliability = c.Int(nullable: false),
                        OnWord = c.Int(nullable: false),
                        Behaviour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingOwnerId);
            
            CreateTable(
                "dbo.RatingWorkers",
                c => new
                    {
                        RatingWorkerId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CommunicationSkill = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        OnBudget = c.Int(nullable: false),
                        Behaviour = c.Int(nullable: false),
                        Completeness = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingWorkerId);
            
            CreateTable(
                "dbo.ResponseToaJobs",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        WUserId = c.Int(nullable: false),
                        FixedPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId);
            
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
                        SelectedWorkerId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SelectedWorkerId);
            
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
                        WorkerInfoId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EarnedMoney = c.Single(nullable: false),
                        RatePerHour = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.WorkerInfoId);
            
            CreateTable(
                "dbo.WorkerSkills",
                c => new
                    {
                        WorkerSkillId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkerSkillId);
            
            CreateTable(
                "dbo.WorkHistories",
                c => new
                    {
                        WorkHistoryId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        Experience = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.WorkHistoryId);
            
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

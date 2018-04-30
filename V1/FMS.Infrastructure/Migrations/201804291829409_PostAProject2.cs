namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAProject2 : DbMigration
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
                "dbo.SavedFiles",
                c => new
                    {
                        SavedFileId = c.Int(nullable: false, identity: true),
                        ProjectSectionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        FileLink = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SavedFileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SavedFiles");
            DropTable("dbo.Payments");
            DropTable("dbo.COMMENTSECs");
        }
    }
}

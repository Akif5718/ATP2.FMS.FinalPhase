namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingOwner : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RatingOwners");
        }
    }
}

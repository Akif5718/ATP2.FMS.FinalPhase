namespace FMS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postskill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectSkills", "SkillName", c => c.String());
            DropColumn("dbo.ProjectSkills", "SkillId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectSkills", "SkillId", c => c.Int(nullable: false));
            DropColumn("dbo.ProjectSkills", "SkillName");
        }
    }
}

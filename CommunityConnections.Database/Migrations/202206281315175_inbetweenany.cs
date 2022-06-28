namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inbetweenany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "InBetweenAny", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sections", "BeforeSection", c => c.String());
            AddColumn("dbo.Sections", "AfterSection", c => c.String());
            DropColumn("dbo.Sections", "After");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "After", c => c.String());
            DropColumn("dbo.Sections", "AfterSection");
            DropColumn("dbo.Sections", "BeforeSection");
            DropColumn("dbo.Sections", "InBetweenAny");
        }
    }
}

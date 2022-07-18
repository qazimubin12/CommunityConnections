namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adfieldsupdated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ads", "Layout");
            DropColumn("dbo.Ads", "ChoosePage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "ChoosePage", c => c.Int(nullable: false));
            AddColumn("dbo.Ads", "Layout", c => c.String());
        }
    }
}

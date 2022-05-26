namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pagetwo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "PageTwo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "PageTwo");
        }
    }
}

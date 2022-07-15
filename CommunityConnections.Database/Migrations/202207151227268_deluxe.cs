namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deluxe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Deluxe", c => c.Boolean(nullable: false));
            DropColumn("dbo.Ads", "Delux");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "Delux", c => c.String());
            DropColumn("dbo.Ads", "Deluxe");
        }
    }
}

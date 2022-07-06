namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totoal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Total", c => c.Single(nullable: false));
            DropColumn("dbo.Ads", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "TotalPrice", c => c.String());
            DropColumn("dbo.Ads", "Total");
        }
    }
}

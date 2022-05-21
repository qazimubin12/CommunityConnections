namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "AdStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "AdStatus");
        }
    }
}

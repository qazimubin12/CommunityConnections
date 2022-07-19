namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "AdDescription", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "AdDescription");
        }
    }
}

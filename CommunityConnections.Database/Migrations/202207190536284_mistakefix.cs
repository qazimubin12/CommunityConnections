namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mistakefix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ads", "AdDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ads", "AdDescription", c => c.Int(nullable: false));
        }
    }
}

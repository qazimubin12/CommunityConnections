namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "AdDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "AdDate");
        }
    }
}

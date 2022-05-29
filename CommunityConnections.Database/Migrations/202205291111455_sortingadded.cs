namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sortingadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "Sort");
        }
    }
}

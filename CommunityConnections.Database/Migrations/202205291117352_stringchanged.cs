namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringchanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ads", "Sort", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ads", "Sort", c => c.Int(nullable: false));
        }
    }
}

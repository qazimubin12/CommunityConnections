namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "Status");
        }
    }
}

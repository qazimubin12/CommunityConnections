namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deluxadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Delux", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "Delux");
        }
    }
}

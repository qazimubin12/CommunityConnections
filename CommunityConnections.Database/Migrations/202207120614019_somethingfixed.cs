namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somethingfixed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "CustomPricing");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomPricing", c => c.String());
        }
    }
}

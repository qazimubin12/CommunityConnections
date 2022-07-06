namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Book", c => c.String());
            AddColumn("dbo.Ads", "Repeat", c => c.String());
            AddColumn("dbo.Ads", "Customer", c => c.String());
            AddColumn("dbo.Ads", "ChoosePage", c => c.Int(nullable: false));
            AddColumn("dbo.Ads", "AddGraphics", c => c.String());
            AddColumn("dbo.Ads", "CustomSpecification", c => c.String());
            AddColumn("dbo.Ads", "Discount", c => c.Single(nullable: false));
            AddColumn("dbo.Ads", "TotalPrice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "TotalPrice");
            DropColumn("dbo.Ads", "Discount");
            DropColumn("dbo.Ads", "CustomSpecification");
            DropColumn("dbo.Ads", "AddGraphics");
            DropColumn("dbo.Ads", "ChoosePage");
            DropColumn("dbo.Ads", "Customer");
            DropColumn("dbo.Ads", "Repeat");
            DropColumn("dbo.Ads", "Book");
        }
    }
}

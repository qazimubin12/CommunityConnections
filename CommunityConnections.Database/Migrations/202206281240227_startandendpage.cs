namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class startandendpage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "StartPage", c => c.Int(nullable: false));
            AddColumn("dbo.Sections", "EndPage", c => c.Int(nullable: false));
            DropColumn("dbo.Sections", "NoOfPages");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "NoOfPages", c => c.Int(nullable: false));
            DropColumn("dbo.Sections", "EndPage");
            DropColumn("dbo.Sections", "StartPage");
        }
    }
}

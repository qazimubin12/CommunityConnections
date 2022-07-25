namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sectionfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Book", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "Book");
        }
    }
}

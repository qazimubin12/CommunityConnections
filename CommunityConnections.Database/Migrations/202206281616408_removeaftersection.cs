namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeaftersection : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sections", "AfterSection");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "AfterSection", c => c.String());
        }
    }
}

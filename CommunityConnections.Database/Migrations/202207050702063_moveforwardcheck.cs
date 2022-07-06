namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moveforwardcheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "MoveForward", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "MoveForward");
        }
    }
}

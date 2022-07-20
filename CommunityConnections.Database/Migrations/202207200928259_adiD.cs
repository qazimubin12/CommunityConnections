namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adiD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AdID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "AdID");
        }
    }
}

namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booksrectified : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "AdID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "AdID", c => c.Int(nullable: false));
        }
    }
}

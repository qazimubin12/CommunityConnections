namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carddatatypechanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cards", "ExpirationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cards", "ExpirationDate", c => c.String());
        }
    }
}

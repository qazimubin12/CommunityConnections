namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class custompricing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomPricings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Customer = c.String(),
                        AdSize = c.String(),
                        Price = c.Single(nullable: false),
                        CustomNotes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomPricings");
        }
    }
}

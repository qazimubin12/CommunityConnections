namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adadds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PageNo = c.Int(nullable: false),
                        Layout = c.String(),
                        AdSize = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ads");
        }
    }
}

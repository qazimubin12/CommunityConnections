namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class section : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SectionName = c.String(),
                        NoOfPages = c.Int(nullable: false),
                        After = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sections");
        }
    }
}

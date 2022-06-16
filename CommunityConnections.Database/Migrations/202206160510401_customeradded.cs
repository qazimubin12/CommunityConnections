namespace CommunityConnections.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customeradded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Compnay = c.String(),
                        Title = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        AreaCode = c.String(),
                        Phone = c.String(),
                        OtherPhone = c.String(),
                        Email = c.String(),
                        Fax = c.String(),
                        BillingEmail = c.String(),
                        PaymentMethod = c.String(),
                        CustomerBalance = c.Single(nullable: false),
                        Notes = c.String(),
                        PopupMessage = c.String(),
                        CustomPricing = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}

namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingpaymenttypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            AddColumn("dbo.OrderItems", "PaymentType_Id", c => c.Guid());
            CreateIndex("dbo.OrderItems", "PaymentType_Id");
            AddForeignKey("dbo.OrderItems", "PaymentType_Id", "dbo.PaymentTypes", "Id");
            DropColumn("dbo.OrderItems", "OrderedOn");
            DropColumn("dbo.OrderItems", "PaymentMethod");
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "Payment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Payment", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "PaymentMethod", c => c.String());
            AddColumn("dbo.OrderItems", "PaymentMethod", c => c.String());
            AddColumn("dbo.OrderItems", "OrderedOn", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.OrderItems", "PaymentType_Id", "dbo.PaymentTypes");
            DropIndex("dbo.PaymentTypes", new[] { "IsDeleted" });
            DropIndex("dbo.OrderItems", new[] { "PaymentType_Id" });
            DropColumn("dbo.OrderItems", "PaymentType_Id");
            DropTable("dbo.PaymentTypes");
        }
    }
}

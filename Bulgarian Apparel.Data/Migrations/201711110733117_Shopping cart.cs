namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shoppingcart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderedOn = c.DateTime(nullable: false),
                        PaymentMethod = c.String(),
                        Payment = c.Double(nullable: false),
                        Address = c.String(),
                        ProductId = c.Guid(nullable: false),
                        ProductSize = c.String(),
                        ProductColor = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Order_Id = c.Guid(),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Order_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.ShoppingCartProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ShoppingCartId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        ProductSize = c.String(),
                        ProductColor = c.String(),
                        ProductPrice = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId, cascadeDelete: true)
                .Index(t => t.ShoppingCartId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            DropColumn("dbo.Orders", "ProductId");
            DropColumn("dbo.Orders", "ProductSize");
            DropColumn("dbo.Orders", "ProductColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ProductColor", c => c.String());
            AddColumn("dbo.Orders", "ProductSize", c => c.String());
            AddColumn("dbo.Orders", "ProductId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.ShoppingCartProducts", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.OrderItems", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ShoppingCarts", new[] { "IsDeleted" });
            DropIndex("dbo.ShoppingCartProducts", new[] { "IsDeleted" });
            DropIndex("dbo.ShoppingCartProducts", new[] { "ShoppingCartId" });
            DropIndex("dbo.OrderItems", new[] { "Customer_Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.OrderItems", new[] { "IsDeleted" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.ShoppingCartProducts");
            DropTable("dbo.OrderItems");
        }
    }
}

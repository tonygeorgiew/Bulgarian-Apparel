namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseRemodeling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SuperCategoryId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Resource = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemColors",
                c => new
                    {
                        Item_Id = c.Int(nullable: false),
                        Color_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Item_Id, t.Color_Id })
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Color_Id, cascadeDelete: true)
                .Index(t => t.Item_Id)
                .Index(t => t.Color_Id);

            CreateTable(
                "dbo.ItemSizes",
                c => new
                {
                    Item_Id = c.Int(nullable: false),
                    Size_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Item_Id, t.Size_Id })
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.Size_Id, cascadeDelete: true)
                .Index(t => t.Item_Id)
                .Index(t => t.Size_Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Image_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Image_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Image_Id);
            
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Supplier", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Products", "ProductTypeId");
            DropColumn("dbo.Products", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImagePath", c => c.String());
            AddColumn("dbo.Products", "ProductTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            DropForeignKey("dbo.ProductImages", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.SizeItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.SizeItems", "Size_Id", "dbo.Sizes");
            DropForeignKey("dbo.ItemColors", "Color_Id", "dbo.Colors");
            DropForeignKey("dbo.ItemColors", "Item_Id", "dbo.Items");
            DropIndex("dbo.ProductImages", new[] { "Image_Id" });
            DropIndex("dbo.ProductImages", new[] { "Product_Id" });
            DropIndex("dbo.SizeItems", new[] { "Item_Id" });
            DropIndex("dbo.SizeItems", new[] { "Size_Id" });
            DropIndex("dbo.ItemColors", new[] { "Color_Id" });
            DropIndex("dbo.ItemColors", new[] { "Item_Id" });
            AlterColumn("dbo.Products", "Name", c => c.String());
            DropColumn("dbo.Products", "Description");
            DropColumn("dbo.Products", "Supplier");
            DropColumn("dbo.Products", "ItemId");
            DropColumn("dbo.Products", "CategoryId");
            DropTable("dbo.ProductImages");
            DropTable("dbo.SizeItems");
            DropTable("dbo.ItemColors");
            DropTable("dbo.Images");
            DropTable("dbo.Sizes");
            DropTable("dbo.Items");
            DropTable("dbo.Colors");
            DropTable("dbo.Categories");
        }
    }
}

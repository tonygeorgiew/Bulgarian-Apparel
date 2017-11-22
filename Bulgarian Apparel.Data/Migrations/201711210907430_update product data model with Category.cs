namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproductdatamodelwithCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Category_Id", c => c.Guid());
            CreateIndex("dbo.Products", "Category_Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id");
            DropColumn("dbo.Products", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropColumn("dbo.Products", "Category_Id");
        }
    }
}

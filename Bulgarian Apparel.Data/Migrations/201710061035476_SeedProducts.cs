namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "DeletedOn", c => c.DateTime());
            DropColumn("dbo.Products", "ProductType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductType", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "DeletedOn");
            DropColumn("dbo.Products", "IsDeleted");
        }
    }
}

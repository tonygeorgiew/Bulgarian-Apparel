namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productDataModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Price");
        }
    }
}

namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categorieswithsupername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "SuperCategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "SuperCategoryName");
        }
    }
}

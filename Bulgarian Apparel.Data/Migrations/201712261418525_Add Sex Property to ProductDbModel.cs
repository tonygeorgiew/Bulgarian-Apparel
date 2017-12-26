namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSexPropertytoProductDbModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Sex", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Sex");
        }
    }
}

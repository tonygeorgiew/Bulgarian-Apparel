namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "Customer_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_Customer_Id");
            DropColumn("dbo.Orders", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.Guid(nullable: false));
            RenameIndex(table: "dbo.Orders", name: "IX_Customer_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Orders", name: "Customer_Id", newName: "User_Id");
        }
    }
}

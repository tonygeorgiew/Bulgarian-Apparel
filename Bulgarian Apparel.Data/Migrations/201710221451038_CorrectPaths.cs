namespace Bulgarian_Apparel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectPaths : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/1.jpg' WHERE Id = 1");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/2.jpg' WHERE Id = 2");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/3.jpg' WHERE Id = 3");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/4.jpg' WHERE Id = 4");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/5.jpg' WHERE Id = 5");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/6.jpg' WHERE Id = 6");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/7.jpg' WHERE Id = 7");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/8.jpg' WHERE Id = 8");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/9.jpg' WHERE Id = 9");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/10.jpg' WHERE Id = 10");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/11.jpg' WHERE Id = 11");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/12.jpg' WHERE Id = 12");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/13.jpg' WHERE Id = 13");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/14.jpg' WHERE Id = 14");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/15.jpg' WHERE Id = 15");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/16.jpg' WHERE Id = 16");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/17.jpg' WHERE Id = 17");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/18.jpg' WHERE Id = 18");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/19.jpg' WHERE Id = 19");
            Sql("UPDATE dbo.Products SET ImagePath = '/Content/Products/20.jpg' WHERE Id = 20");
        }
        
        public override void Down()
        {
        }
    }
}

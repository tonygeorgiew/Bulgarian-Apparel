namespace Bulgarian_Apparel.Data.Migrations
{
    using Bulgarian_Apparel.Data.Models;
    using Bulgarian_Apparel.Providers.Contracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        const string AdministratorUserName = "antonii.g@mail.bg";
        const string AccountantUserName = "accountant@apparel.bg";
        const string AdministratorPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            //  this.SeedUsers(context);
            //  this.SeedSampleData(context);
            //  this.SeedSampleCategories(context);
            //  this.SeedSampleColors(context);
            //  this.SeedSampleSizes(context);
            //  this.SeedSampleImages(context);
            //  this.SeedSampleProducts(context);
            //  this.seedPaymentTypes(context);
            //  this.SeedSampleItems(context);
            //  this.SeedNewProductModel(context);
           // this.SeedAccountantRole(context);


            base.Seed(context);
        }

        private void SeedAccountantRole(MsSqlDbContext context)
        {
           
                var roleName = "Accountant";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AccountantUserName,
                    Email = AccountantUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, roleName);
            
        }

        private void SeedPaymentTypes(MsSqlDbContext context)
        {
            var PayPal = new PaymentType() { Name = "PayPal" };
            var CreditCard = new PaymentType() { Name = "Credit Card" };

            context.PaymentTypes.Add(PayPal);
            context.PaymentTypes.Add(CreditCard);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleName = "Admin";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, roleName);
            }
        }
        

        private void SeedSampleCategories(MsSqlDbContext context)
        {
            // Seeding the regular categories

           //  var categoryNames = new string[]
           //  {
           //      "Shoes",
           //      "Socks",
           //      "Shirts",
           //      "Pants",
           //      "Jackets",
           //      "Sweatshirt",
           //      "Suits",
           //  };
           // 
           //  for (int i = 0; i < categoryNames.Length; i++)
           //  {
           //      var category = new Category
           //      {
           //          Name = categoryNames[i]
           //      };
           // 
           //      context.Categories.Add(category);
           //  }

            

            // Now seed the super categories

            var categoryNames = new string[]
            {
                "Long-sleeved shirts",
                "Blazer"
            };

            var tshirtCategory = new Category
            {
                Name = categoryNames[0],
                SuperCategoryId = Guid.Parse("32875EED-C798-4745-8777-D1AC7F40CEB0")
            };
            
            var blazerCategory = new Category
            {
                Name = categoryNames[1],
                SuperCategoryId = Guid.Parse("E2F42933-07A2-4284-88C3-0291902F9E66")
            };
            
            context.Categories.Add(tshirtCategory);
           // context.Categories.Add(blazerCategory);
        }

        private void SeedSampleColors(MsSqlDbContext context)
        {
            var colorNames = new string[]
            {
                "Red",
                "Blue",
                "Green",
                "Yellow",
                "Orange",
                "Black",
                "Purple",
                "Brown",
                "Gray"
            };

            for (int i = 0; i < colorNames.Length; i++)
            {
                var colorToBeAdded = new Color()
                {
                    Name = colorNames[i]
                };

                context.Colors.Add(colorToBeAdded);
            }
            
        }

        private void SeedSampleSizes(MsSqlDbContext context)
        {
            var sizeNames = new string[]
                   {
                "XXS",
                "XS",
                "S",
                "M",
                "L",
                "XL",
                "XXL",
                "XXXL"
                   };

            for (int i = 0; i < sizeNames.Length; i++)
            {
                var sizeToBeAdded = new Size()
                {
                    Name = sizeNames[i]
                };

                context.Sizes.Add(sizeToBeAdded);
            }

        }

        private void SeedSampleImages(MsSqlDbContext context)
        {
            for (int i = 1; i < 21; i++)
            {
                var image = new Image()
                {
                   Resource = string.Format("/Content/Products/{0}.jpg", i) 
                };

                context.Images.Add(image);
            }
        }

       
        private void SeedSampleProducts(MsSqlDbContext context)
        {
            Guid guid = new Guid("D44FA454-F489-4A77-9884-4A335C7C6F0D");
            Guid image1Guid = new Guid("09DD6EBC-8CD7-47EB-99B1-2E35A8538FD0");
            Guid image2Guid = new Guid("9AE75984-1EA5-4B0B-9348-4B9DC1A1591C");
            Guid image3Guid = new Guid("E2515A80-AE08-4A32-9BE5-B2B05D9B6409");

            var product = context.Products.Where(p => p.Id == guid).Single() as Product;

            var image1 = context.Images.Where(i => i.Id == image1Guid).Single() as Image;
            var image2 = context.Images.Where(i => i.Id == image2Guid).Single() as Image;
            var image3 = context.Images.Where(i => i.Id == image3Guid).Single() as Image;

            image1.FileName = "Zoomed Blue Shirt";
            image2.FileName = "Model Blue Shirt";
            image3.FileName = "Full Blue Shirt";

            product.Images.Add(image1);
            product.Images.Add(image2);
            product.Images.Add(image3);

            context.Products.AddOrUpdate(product);
        }

       
        private void SeedNewProductModel(MsSqlDbContext context)
        {
            var item = new Item()
            {
                Price = 45.99,
            };

            var white = new Color()
            {
                Name = "White"
            };

            var black = context.Colors.Where(c => c.Name == "Black").Single();


            var xs = context.Sizes.Where(s => s.Name == "XS").Single();
            var ss = context.Sizes.Where(s => s.Name == "S").Single();
            var m = context.Sizes.Where(s => s.Name == "M").Single();
            var l = context.Sizes.Where(s => s.Name == "L").Single();
            var xl = context.Sizes.Where(s => s.Name == "XL").Single();
            item.Sizes.Add(m);
            item.Sizes.Add(l);
            item.Sizes.Add(ss);
            item.Sizes.Add(xl);
            item.Sizes.Add(xs);
            item.Colors.Add(black);
            item.Colors.Add(white);

            var product = new Product()
            {
                Name = "Black and White shirt",
                Description = "Comfortable and good looking for every kind of need.",
                Supplier = "H&M",
                //CategoryId = Guid.Parse("C2C65225-C5B8-420C-95EF-1CA2B92D73FD"),
                Stock = 14,
                Hot = true,
            };
            var guid1 = Guid.Parse("34BB74F7-2059-490B-BBC9-34F2B071615F");
            var guid2 = Guid.Parse("7B65EBB2-CA2E-48E0-B25F-920276AEE041");
            var guid3 = Guid.Parse("C0F1972C-5FDB-42C9-9584-39D433E52123");
            var image1 = context.Images.Single(i => i.Id == guid1);
            var image2 = context.Images.Single(i => i.Id == guid2);
            var image3 = context.Images.Single(i => i.Id == guid3);
            product.Images.Add(image1);
            product.Images.Add(image2);
            product.Images.Add(image3);
            product.ItemId = item.Id;
            item.ProductId = product.Id;

            context.Items.Add(item);
            context.Products.Add(product);
        }
    }
}

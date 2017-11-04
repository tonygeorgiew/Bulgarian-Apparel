namespace Bulgarian_Apparel.Data.Migrations
{
    using Bulgarian_Apparel.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        const string AdministratorUserName = "antonii.g@mail.bg";
        const string AdministratorPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            //  this.SeedUsers(context);
            //  this.SeedSampleData(context);
            //  this.SeedSampleCategories(context);
            //  this.SeedSampleColors(context);
            //  this.SeedSampleSizes(context);
            //  this.SeedSampleImages(context);
            //    this.SeedSampleProducts(context);
           
            //  this.SeedSampleItems(context);

            base.Seed(context);
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

       
        private void SeedSampleItems(MsSqlDbContext context)
        {
            var item = new Item()
            {
                Price = 22.99
            };

            var purple = context.Colors.Where(c => c.Name == "Purple").Single();
            var black = context.Colors.Where(c => c.Name == "Black").Single();

            var svar = context.Sizes.Where(s => s.Name == "S").Single();
            var m = context.Sizes.Where(s => s.Name == "M").Single();
            var l = context.Sizes.Where(s => s.Name == "L").Single();
            var xl = context.Sizes.Where(s => s.Name == "XL").Single();


            item.Colors.Add(purple);
            item.Colors.Add(black);

            item.Sizes.Add(svar);
            item.Sizes.Add(m);
            item.Sizes.Add(l);
            item.Sizes.Add(xl);


            context.Items.Add(item);
            


            // product.Images.Add()
        }
    }
}

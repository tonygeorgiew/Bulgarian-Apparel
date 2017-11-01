﻿namespace Bulgarian_Apparel.Data
{
    using Bulgarian_Apparel.Data.Models;
    using Bulgarian_Apparel.Data.Models.Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MsSqlDbContext : IdentityDbContext<User>
    {
        
        public MsSqlDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {

        }
      

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Color> Colors { get; set; }

        public IDbSet<Size> Sizes { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Post> Posts { get; set; }
        

        public override int SaveChanges()
        {
            ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }
    }
}

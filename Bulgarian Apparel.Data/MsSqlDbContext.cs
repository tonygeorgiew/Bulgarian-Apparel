namespace Bulgarian_Apparel.Data
{
    using Bulgarian_Apparel.Data.Models;
    using Bulgarian_Apparel.Data.Models.Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;  

    public class MsSqlDbContext : IdentityDbContext<User>, IMsSqlDbContext
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

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<OrderItem> OrderItems { get; set; }

        public IDbSet<ShoppingCart> ShoppingCarts { get; set; }

        public IDbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        public IDbSet<PaymentType> PaymentTypes { get; set; }

        public IDbSet<WishlistItem> WishlistItems { get; set; }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }

        public override int SaveChanges()
        {
            ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public IDbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
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
    }
}

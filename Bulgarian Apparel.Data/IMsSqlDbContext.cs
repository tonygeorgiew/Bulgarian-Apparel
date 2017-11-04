namespace Bulgarian_Apparel.Data
{
    using Bulgarian_Apparel.Data.Models;
    using System.Data.Entity;

    public interface IMsSqlDbContext
    {
        int SaveChanges();

        IDbSet<Product> Products { get; set; }

        IDbSet<Item> Items { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Color> Colors { get; set; }

        IDbSet<Size> Sizes { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Post> Posts { get; set; }

        IDbSet<TEntity> DbSet<TEntity>() where TEntity : class;
    }
}
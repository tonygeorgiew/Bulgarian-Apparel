namespace Bulgarian_Apparel.Services
{

    using Bulgarian_Apparel.Data.Models;
    using Bulgarian_Apparel.Data.Models.Contracts;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly IEfRepository<Product> productsRepo;

        public ProductsService(IEfRepository<Product> productsRepo)
        {
            this.productsRepo = productsRepo;
        }

        public IQueryable<Product> GetAll()
        {
            return this.productsRepo.All;
        }

        public IQueryable<Product> GetCategory(int id)
        {
            return null;
        }
    }
}

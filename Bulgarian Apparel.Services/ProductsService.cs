using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services
{
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
    }
}

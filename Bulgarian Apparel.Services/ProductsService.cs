namespace Bulgarian_Apparel.Services
{

    using Bulgarian_Apparel.Data.Models;
    using Bulgarian_Apparel.Data.Models.Contracts;
    using Bulgarian_Apparel.Data.SaveContext;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsService : IProductsService
    {
        private readonly IEfRepository<Product> productsRepo;
        private readonly IEfRepository<Item> itemsRepo;
        private readonly IEfRepository<Category> categoriesRepo;
        private readonly IEfRepository<Color> colorsRepo;
        private readonly IEfRepository<Size> sizesRepo;
        private readonly IEfRepository<Image> imagesRepo;
        private readonly IUnitOfWork UoW;

        public ProductsService(
            IEfRepository<Product> productsRepo, 
            IEfRepository<Item> itemsRepo,
            IEfRepository<Category> categoriesRepo, 
            IEfRepository<Color> colorsRepo, 
            IEfRepository<Size> sizesRepo, 
            IEfRepository<Image> imagesRepo,
            IUnitOfWork UoW )
        {
            this.productsRepo = productsRepo;
            this.itemsRepo = itemsRepo;
            this.categoriesRepo = categoriesRepo;
            this.colorsRepo = colorsRepo;
            this.sizesRepo = sizesRepo;
            this.imagesRepo = imagesRepo;
            this.UoW = UoW;
        }

        public int Add(Product product)
        {
            this.productsRepo.Add(product);
            return this.UoW.Commit();
        }

        public IQueryable<Product> ProducttById(Guid id)
        {
            var query = this.productsRepo.All.Where(p => p.Id == id);

            return query;
        }


        public IQueryable<Product> GetAll()
        {
            return this.productsRepo.All.Include(i => i.Images);
        }

        public IQueryable<Product> ProductWithImagesById(Guid id)
        {
            return this.ProducttById(id).Include(i=>i.Images);
        }

        public IQueryable<Product> GetProductsForCategoryGuid(Guid id)
        {
            var products = this.productsRepo.All.Where(p => p.CategoryId == id);

            return products;
        }
    }
}

namespace Bulgarian_Apparel.Services
{

    using Bulgarian_Apparel.Data.Models;
    using Bulgarian_Apparel.Data.Models.Contracts;
    using Bulgarian_Apparel.Data.SaveContext;
    using Bulgarian_Apparel.Services.Contracts;
    using Bulgarian_Apparel.Web.Infrastructure;
    using System;
    using System.Data.Entity;
    using System.Linq;

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

        public int Delete(Product product)
        {
            this.productsRepo.Delete(product);

            return this.UoW.Commit();
        }

        public int Update(Product product)
        {
            this.productsRepo.Update(product);

            return this.UoW.Commit();
        }

        public IQueryable<Product> GetById(Guid? id)
        {
            var query = this.productsRepo
                .All
                .Where(p => p.Id == id)
                .Include( p => p.Images );

            return query;
        }

        public IQueryable<Product> GetByStringId(string id)
        {
            Guid productGuid = IdProccessor.GetGuidForStringId(id);
            var query = this.productsRepo
                .All
                .Where(p => p.Id == productGuid)
                .Include(p => p.Images);

            return query;
        }

        public IQueryable<Product> GetHotProducts()
        {
            var query = this.productsRepo
                .All
                .Where(p => p.Hot == true);

            return query;
        }


        public IQueryable<Product> GetAll()
        {
            return this.productsRepo
                .All
                .Include(i => i.Images)
                .Include(c=>c.Category);
        }
        

        public IQueryable<Product> ProductWithImagesById(Guid id)
        {
            return this.GetById(id)
                .Include(i=>i.Images);
        }
    }
}

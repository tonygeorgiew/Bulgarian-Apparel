using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IEfRepository<Product> productsRepo;
        private readonly IEfRepository<Item> itemsRepo;
        private readonly IEfRepository<Color> colorsRepo;
        private readonly IEfRepository<Size> sizesRepo;

        public ItemsService(
            IEfRepository<Product> productsRepo,
            IEfRepository<Item> itemsRepo,
            IEfRepository<Color> colorsRepo,
            IEfRepository<Size> sizesRepo)
        {
            this.productsRepo = productsRepo;
            this.itemsRepo = itemsRepo;
            this.colorsRepo = colorsRepo;
            this.sizesRepo = sizesRepo;
        }

        public IQueryable<Item> GetAll()
        {
            return this.itemsRepo.All
                .Include(s => s.Sizes)
                .Include(c => c.Colors);
        }
    }
}

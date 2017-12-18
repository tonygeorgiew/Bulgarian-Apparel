using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.SaveContext;
using Bulgarian_Apparel.Services.Contracts;
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
        private readonly IUnitOfWork UoW;

        public ItemsService(
            IEfRepository<Product> productsRepo,
            IEfRepository<Item> itemsRepo,
            IEfRepository<Color> colorsRepo,
            IEfRepository<Size> sizesRepo,
            IUnitOfWork UnitOfWork)
        {
            this.productsRepo = productsRepo;
            this.itemsRepo = itemsRepo;
            this.colorsRepo = colorsRepo;
            this.sizesRepo = sizesRepo;
            this.UoW = UnitOfWork;
        }

        public IQueryable<Item> GetAll()
        {
            return this.itemsRepo.All
                .Include(s => s.Sizes)
                .Include(c => c.Colors);
        }

        public int Add(Item item)
        {
            this.itemsRepo.Add(item);

            return this.UoW.Commit();
        }

        public int Delete(Item item)
        {
            this.itemsRepo.Delete(item);

            return this.UoW.Commit();
        }

        public int Update(Item item)
        {
            this.itemsRepo.Update(item);

            return this.UoW.Commit();
        }

    }
}

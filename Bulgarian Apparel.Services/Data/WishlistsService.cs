using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.SaveContext;
using Bulgarian_Apparel.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services.Data
{
    public class WishlistsService : IWishlistsService
    {
        private readonly IEfRepository<WishlistItem> wishlistItemsRepo;
        private readonly IUnitOfWork UoW;

        public WishlistsService(IEfRepository<WishlistItem> wishlistItemsRepo, IUnitOfWork UoW)
        {
            this.wishlistItemsRepo = wishlistItemsRepo ?? throw new ArgumentNullException(nameof(wishlistItemsRepo));
            this.UoW = UoW;
        }

        public IQueryable<WishlistItem> GetAll()
        {
            return this.wishlistItemsRepo.All;
        }

        public int Add(WishlistItem wish)
        {
            this.wishlistItemsRepo.Add(wish);

            return this.UoW.Commit();
        }

        public int Delete(WishlistItem wish)
        {
            this.wishlistItemsRepo.Delete(wish);

            return this.UoW.Commit();
        }

        public int Update(WishlistItem wish)
        {
            this.wishlistItemsRepo.Update(wish);

            return this.UoW.Commit();
        }

    }
}

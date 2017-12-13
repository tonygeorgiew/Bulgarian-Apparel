using Bulgarian_Apparel.Data.Models;
using System.Linq;

namespace Bulgarian_Apparel.Services.Data.Contracts
{
    public interface IWishlistsService
    {
        IQueryable<WishlistItem> GetAll();

        int Add(WishlistItem wish);

        int Delete(WishlistItem wish);

        int Update(WishlistItem wish);
    }
}
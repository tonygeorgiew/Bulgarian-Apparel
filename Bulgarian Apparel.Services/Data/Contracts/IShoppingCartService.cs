using Bulgarian_Apparel.Data.Models;
using System;
using System.Linq;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface IShoppingCartService
    {
        IQueryable<ShoppingCart> GetAll();

        IQueryable<ShoppingCart> GetCartForUserId(string Id);

        int Add(ShoppingCart cart);

        int Delete(ShoppingCart cart);

        int Update(ShoppingCart cart);

        double CalculateTotalPriceForCart(string guid);
    }
}
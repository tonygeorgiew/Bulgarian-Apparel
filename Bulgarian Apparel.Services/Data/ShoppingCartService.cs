using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.SaveContext;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Bulgarian_Apparel.Web.Infrastructure;

namespace Bulgarian_Apparel.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IEfRepository<ShoppingCart> shoppingCartRepo;
        private readonly IUnitOfWork UoW;

        public ShoppingCartService(IEfRepository<ShoppingCart> shoppingCartRepo, IUnitOfWork UoW)
        {
            this.shoppingCartRepo = shoppingCartRepo;
            this.UoW = UoW;
        }

        public IQueryable<ShoppingCart> GetAll()
        {
            return this.shoppingCartRepo.All.Include(p => p.ShoppingCartProducts);
        }

        public IQueryable<ShoppingCart> GetCartForUserId(string Id)
        {
            var userGuid = IdProccessor.GetGuidForStringId(Id);
            var query = this.shoppingCartRepo.All.Where(c => c.UserId == userGuid).Include(p => p.ShoppingCartProducts);

            return query;
        }

        public int Add(ShoppingCart cart)
        {
            this.shoppingCartRepo.Add(cart);

            return this.UoW.Commit();
        }

        public int Delete(ShoppingCart cart)
        {
            this.shoppingCartRepo.Delete(cart);

            return this.UoW.Commit();
        }

        public int Update(ShoppingCart cart)
        {
            this.shoppingCartRepo.Update(cart);

            return this.UoW.Commit();
        }

        public double CalculateTotalPriceForCart(Guid guid)
        {
            return this.shoppingCartRepo
                .All
                .Where(u => u.IsDeleted == false)
                .Single(cart => cart.UserId == guid)
                .ShoppingCartProducts.Sum(price => price.ProductPrice);
        }
    }
}

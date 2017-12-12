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
    public class OrdersItemsService : IOrdersItemsService
    {
        private readonly IEfRepository<OrderItem> ordersItemsRepo;
        private readonly IEfRepository<User> usersRepo;
        private readonly IEfRepository<Product> productsRepo;
        private readonly IUnitOfWork UoW;

        public OrdersItemsService(IEfRepository<OrderItem> ordersItemsRepo, IEfRepository<User> usersRepo,IEfRepository<Product> productsRepo,IUnitOfWork UoW)
        {
            this.ordersItemsRepo = ordersItemsRepo;
            this.usersRepo = usersRepo;
            this.productsRepo = productsRepo;
            this.UoW = UoW;
        }

        public IQueryable<OrderItem> All()
        {   
            return this.ordersItemsRepo.All.Include(o => o.Customer).Include(p=>p.Product);
        }

        public int Add(OrderItem order)
        {
            this.ordersItemsRepo.Add(order);

            return this.UoW.Commit();
        }

        public int Delete(OrderItem order)
        {
            this.ordersItemsRepo.Delete(order);

            return this.UoW.Commit();
        }

        public int Update(OrderItem order)
        {
            this.ordersItemsRepo.Update(order);

            return this.UoW.Commit();
        }
    }
}

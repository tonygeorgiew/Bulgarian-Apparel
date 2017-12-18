using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.SaveContext;
using System.Data.Entity;
using Bulgarian_Apparel.Services.Contracts;

namespace Bulgarian_Apparel.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IEfRepository<Order> ordersRepo;
        private readonly IEfRepository<OrderItem> orderItemsRepo;
        private readonly IEfRepository<User> usersRepo;
        private readonly IUnitOfWork UoW;

        public OrdersService(IEfRepository<Order> ordersRepo, IEfRepository<OrderItem> orderItemsRepo, IEfRepository<User> usersRepo ,IUnitOfWork UoW)
        {
            this.ordersRepo = ordersRepo;
            this.orderItemsRepo = orderItemsRepo;
            this.usersRepo = usersRepo;
            this.UoW = UoW;
        }

        public IQueryable<Order> GetAll()
        {
            return this.ordersRepo.All
                .Include(o => o.OrderItems)
                .Include(c => c.Customer);
        }
        public int Add(Order order)
        {
            this.ordersRepo.Add(order);

            return this.UoW.Commit();
        }

        public int Delete(Order order)
        {
            this.ordersRepo.Delete(order);

            return this.UoW.Commit();
        }

        public int Update(Order order)
        {
            this.ordersRepo.Update(order);

            return this.UoW.Commit();
        }
    }
}

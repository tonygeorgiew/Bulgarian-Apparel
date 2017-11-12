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
        private readonly IUnitOfWork UoW;

        public OrdersItemsService(IEfRepository<OrderItem> ordersItemsRepo, IEfRepository<User> usersRepo ,IUnitOfWork UoW)
        {
            this.ordersItemsRepo = ordersItemsRepo;
            this.usersRepo = usersRepo;
            this.UoW = UoW;
        }

        public IQueryable<OrderItem> All()
        {
            return this.ordersItemsRepo.All.Include(o => o.Customer);
        }
    }
}

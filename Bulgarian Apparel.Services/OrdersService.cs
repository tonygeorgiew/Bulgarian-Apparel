using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.SaveContext;

namespace Bulgarian_Apparel.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IEfRepository<Order> ordersRepo;
        private readonly IUnitOfWork UoW;

        public OrdersService(IEfRepository<Order> ordersRepo, IUnitOfWork UoW)
        {
            this.ordersRepo = ordersRepo;
            this.UoW = UoW;
        }

        public IQueryable<Order> GetAll()
        {
            return this.ordersRepo.All;
        }
    }
}

using Bulgarian_Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface IOrdersItemsService
    {
        IQueryable<OrderItem> All();
        IQueryable<OrderItem> GetByStringId(string id);
        IQueryable<OrderItem> GetById(Guid? id);
        IQueryable<OrderItem> GetByCustomerId(string id);
        int Add(OrderItem order);
        int Delete(OrderItem order);
        int Update(OrderItem order);
    }
}

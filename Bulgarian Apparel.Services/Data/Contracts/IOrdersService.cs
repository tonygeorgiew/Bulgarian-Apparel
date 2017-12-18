using Bulgarian_Apparel.Data.Models;
using System.Linq;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface IOrdersService
    {
        IQueryable<Order> GetAll();

        int Add(Order order);

        int Delete(Order order);

        int Update(Order order);
    }
}
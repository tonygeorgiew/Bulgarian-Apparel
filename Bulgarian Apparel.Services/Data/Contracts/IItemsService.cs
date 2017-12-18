using Bulgarian_Apparel.Data.Models;
using System.Linq;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface IItemsService
    {
        IQueryable<Item> GetAll();

        int Add(Item item);

        int Delete(Item item);

        int Update(Item item);
    }
}
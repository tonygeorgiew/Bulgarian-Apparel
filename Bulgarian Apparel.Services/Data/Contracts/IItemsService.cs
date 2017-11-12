using Bulgarian_Apparel.Data.Models;
using System.Linq;

namespace Bulgarian_Apparel.Services
{
    public interface IItemsService
    {
        IQueryable<Item> GetAll();
    }
}
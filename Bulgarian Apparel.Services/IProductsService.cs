using Bulgarian_Apparel.Data.Models;
using System.Linq;

namespace Bulgarian_Apparel.Services
{
    public interface IProductsService
    {
        IQueryable<Product> GetAll();
    }
}
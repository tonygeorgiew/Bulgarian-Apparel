using Bulgarian_Apparel.Data.Models;
using System;
using System.Linq;

namespace Bulgarian_Apparel.Services
{
    public interface IProductsService
    {
        IQueryable<Product> GetAll();

        IQueryable<Product> GetByStringId(string id);

        IQueryable<Product> GetById(Guid? id);

        int Add(Product product);

        int Delete(Product product);

        int Update(Product product);
    }
}
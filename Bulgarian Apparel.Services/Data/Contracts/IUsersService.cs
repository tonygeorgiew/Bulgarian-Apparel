using Bulgarian_Apparel.Data.Models;
using System.Linq;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<User> ByUsername(string username);
        IQueryable<User> GetAll();
        IQueryable<User> GetUserById(string id);

        int Add(User product);

        int Delete(User product);
                   
        int Update(User product);
    }
}
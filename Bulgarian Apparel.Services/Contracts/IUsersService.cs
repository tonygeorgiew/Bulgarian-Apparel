using Bulgarian_Apparel.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services
{
    public interface IUsersService
    {
        IQueryable<User> ByUsername(string username);

        IQueryable<User> All();
    }
}
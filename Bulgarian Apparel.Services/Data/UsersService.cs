using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEfRepository<User> users;

        public UsersService(IEfRepository<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> All()
        {
            return this.users.All;
        }

        public IQueryable<User> ByUsername(string username)
        {
            return this.users
                .All
                .Where(u => u.UserName == username);
        }
    }
}

using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.SaveContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly IUnitOfWork UoW;

        public UsersService(IEfRepository<User> users, IUnitOfWork UoW)
        {
            this.usersRepo = users;
            this.UoW = UoW;
        }
        
        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All;
        }

        public IQueryable<User> ByUsername(string username)
        {
            return this.usersRepo
                .All
                .Where(u => u.UserName == username);
        }
       
        public IQueryable<User> GetUserById(string id)
        {
            return this.usersRepo.All.Where(x => x.Id == id);
        }

        public int Add(User user)
        {
            this.usersRepo.Add(user);

            return this.UoW.Commit();
        }

        public int Delete(User user)
        {
            this.usersRepo.Delete(user);

            return this.UoW.Commit();
        }

        public int Update(User user)
        {
            this.usersRepo.Update(user);

            return this.UoW.Commit();
        }
    }
}

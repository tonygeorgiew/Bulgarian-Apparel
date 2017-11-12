using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services
{
    public class PostsService : IPostsService
    {
        private readonly IEfRepository<Post> postsRepo;

        public PostsService(IEfRepository<Post> postsRepo)
        {
            this.postsRepo = postsRepo;
        }

        public IQueryable<Post> GetAll()
        {
            return this.postsRepo.All;
        }
    }
}

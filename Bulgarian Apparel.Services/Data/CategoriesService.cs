using System;
using System.Linq;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Services.Contracts;

namespace Bulgarian_Apparel.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IEfRepository<Category> categoriesRepo;

        public CategoriesService(IEfRepository<Category> categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo ?? throw new ArgumentNullException(nameof(categoriesRepo)); 
        }

        public IQueryable<Category> GetAll()
        {
            return this.categoriesRepo.All;
        }
    }
}

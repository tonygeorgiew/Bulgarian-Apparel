using Bulgarian_Apparel.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;

namespace Bulgarian_Apparel.Services
{
    public class SizesService : ISizesService
    {
        private readonly IEfRepository<Size> sizesRepo; 

        public SizesService(IEfRepository<Size> sizesRepo)
        {
            this.sizesRepo = sizesRepo ?? throw new ArgumentNullException(nameof(sizesRepo));
        }

        public IQueryable<Size> GetAll()
        {
            return this.sizesRepo.All;
        }
    }
}

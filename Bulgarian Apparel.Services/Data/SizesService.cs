using Bulgarian_Apparel.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Web.Infrastructure;

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

        public Size GetSizeForGuid(Guid id)
        {
            return this.sizesRepo.All.Single(s => s.Id == id);
        }

        public Size GetSizeForStringGuid(string id)
        {
            Guid guidId = IdProccessor.GetGuidForStringId(id);

            return this.sizesRepo.All.Single(s => s.Id == guidId);
        }
    }
}

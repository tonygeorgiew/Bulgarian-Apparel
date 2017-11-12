using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services
{
   public class ColorsService : IColorsService
    {
        private readonly IEfRepository<Color> colorsRepo;

        public ColorsService(IEfRepository<Color> colorsRepo)
        {
            this.colorsRepo = colorsRepo ?? throw new ArgumentNullException(nameof(colorsRepo));
        }

        public IQueryable<Color> GetAll()
        {
            return this.colorsRepo.All;
        }

        public Color GetColorForGuid(Guid id)
        {
            return this.colorsRepo.All.Single(c => c.Id == id);
        }

        public Color GetColorForStringGuid(string id)
        {
            Guid guidId = IdProccessor.GetGuidForStringId(id);

            return this.colorsRepo.All.Single(c => c.Id == guidId);
        }
    }
}

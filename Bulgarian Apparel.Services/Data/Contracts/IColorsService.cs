using Bulgarian_Apparel.Data.Models;
using System;
using System.Linq;

namespace Bulgarian_Apparel.Services.Contracts
{
    public interface IColorsService
    {
        IQueryable<Color> GetAll();

        Color GetColorForGuid(Guid id);

        Color GetColorForStringGuid(string id);
    }
}
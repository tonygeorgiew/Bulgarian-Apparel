using Bulgarian_Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Services.Common.Extensions
{
    public class ColorComparer : IEqualityComparer<Color>
    {
        public bool Equals(Color x, Color y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;


            //Check whether the products' properties are equal. 
            return x != null && y != null && x.Id.Equals(y.Id) && x.Name.Equals(y.Name);
        }

        public int GetHashCode(Color obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();

            //Get hash code for the Code field. 
            int hashProductCode = obj.Id.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }
}

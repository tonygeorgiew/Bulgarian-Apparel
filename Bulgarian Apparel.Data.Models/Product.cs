using Bulgarian_Apparel.Data.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductTypeId { get; set; }
        [EnumDataType(typeof(ProductType))]
        public ProductType ProductType { get; set; }
        public string ImagePath { get; set; }
    }
}

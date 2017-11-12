using Bulgarian_Apparel.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
    public class ShoppingCartProduct : DataModel
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public double ProductPrice { get; set; }
        public bool PayedFor { get; set; }
    }
}

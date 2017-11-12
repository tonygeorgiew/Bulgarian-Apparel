using Bulgarian_Apparel.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
    public class ShoppingCart : DataModel
    {
        public ShoppingCart()
        {
            this.ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
        }

        public virtual Guid UserId { get; set; }

        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}

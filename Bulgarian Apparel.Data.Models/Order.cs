using Bulgarian_Apparel.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
   public class Order : DataModel
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public virtual User Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

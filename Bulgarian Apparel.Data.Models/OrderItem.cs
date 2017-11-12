using Bulgarian_Apparel.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
    public class OrderItem : DataModel
    {
        public DateTime OrderedOn { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public double Payment { get; set; }
        public virtual User Customer { get; set; }
        public string Address { get; set; }
        public Guid ProductId { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }

    }
}

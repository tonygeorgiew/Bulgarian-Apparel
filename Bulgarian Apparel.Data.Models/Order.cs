using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
   public class Order
    {
        public int Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public string PaymentMethod { get; set; }
        public double Payment { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
    }
}

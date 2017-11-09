using System;
using System.ComponentModel.DataAnnotations;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class OrderViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string SizeSelectedId { get; set; }
        public string Color { get; set; }
        public string ColorSelectedId { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime OrderedOn { get; set; }
        public double Price { get; set; }
    }
}
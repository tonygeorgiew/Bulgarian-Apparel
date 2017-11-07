using System;
using System.ComponentModel.DataAnnotations;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class OrderViewModel
    {
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        [Display(Name ="Available sizes")]
        public Guid SizeSelectedId { get; set; }
        [Display(Name = "Available colors")]
        public Guid ColorSelectedId { get; set; }
    }
}
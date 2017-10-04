using Bulgarian_Apparel.Data.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Models.Home
{
    public class ProductSliderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductTypeId { get; set; }
        public string ImagePath { get; set; }
    }
}
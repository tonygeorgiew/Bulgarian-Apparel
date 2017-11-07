using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class ProductFormViewModel
    {
        public IEnumerable<SelectListItem> SizesAvailable { get; set; }
        public ProductViewModel Product { get; set; }
        public OrderViewModel Order { get; set; }
    }
}
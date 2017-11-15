using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using Bulgarian_Apparel.Web.Infrastructure;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class ProductFormViewModel
    {
        public ProductViewModel Product { get; set; }
        public OrderViewModel Order { get; set; }
    }
}
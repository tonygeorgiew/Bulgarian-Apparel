using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Areas.Admin.Models
{
    public class UpdateProductInformationViewModel
    {
        public UpdateProductViewModel UpdateProduct { get; set; }
        
        public ProductInformationViewModel ProductInformation { get; set; }
    }
}
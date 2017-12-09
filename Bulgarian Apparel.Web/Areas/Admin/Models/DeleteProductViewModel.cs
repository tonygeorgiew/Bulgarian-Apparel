using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Areas.Admin.Models
{
    public class DeleteProductViewModel : IMapFrom<Product>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "Product Name")]
        public string Supplier { get; set; }
    }
}
using Bulgarian_Apparel.Data.Models.Abstracts;
using Bulgarian_Apparel.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
    public class Image : DataModel
    {
        public Image()
        {
            this.Products = new HashSet<Product>();
        }

        public string FileName { get; set; }

        [Required]
        public string Resource { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

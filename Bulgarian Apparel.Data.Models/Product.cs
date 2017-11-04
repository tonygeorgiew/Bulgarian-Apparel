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
    public class Product : DataModel
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
        }
      
        public Guid CategoryId { get; set; }

        public Guid ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Supplier { get; set; }

        [Display(Name= "Picture Resource")]
        public ICollection<Image> Images { get; set; }

        //TO DO: add bool available and int Stock properties

        public string Description { get; set; }
    }
}

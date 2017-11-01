using Bulgarian_Apparel.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data.Models
{
    public class Product : IDeletable
    {

        public Product()
        {
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name="Type")]
        public int CategoryId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Supplier { get; set; }

        [Required]
        [Display(Name= "Picture Resource")]
        public ICollection<Image> Images { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

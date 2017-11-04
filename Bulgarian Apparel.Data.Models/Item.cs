namespace Bulgarian_Apparel.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Bulgarian_Apparel.Data.Models.Contracts;
    using System;
    using Bulgarian_Apparel.Data.Models.Abstracts;

    public class Item : DataModel
    {

        public Item()
        {
            this.Sizes = new HashSet<Size>();
            this.Colors = new HashSet<Color>();
        }
        
        public Guid ProductId { get; set; }

        public virtual ICollection<Size> Sizes { get; set; }

        public virtual ICollection<Color> Colors { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
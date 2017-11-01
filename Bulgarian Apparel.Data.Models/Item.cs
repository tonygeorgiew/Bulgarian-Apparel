namespace Bulgarian_Apparel.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Item
    {

        public Item()
        {
            this.Sizes = new HashSet<Size>();
            this.Colors = new HashSet<Color>();
        }

        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual ICollection<Size> Sizes { get; set; }

        public virtual ICollection<Color> Colors { get; set; }

        [Required]
        public double Price { get; set; }
    }
}

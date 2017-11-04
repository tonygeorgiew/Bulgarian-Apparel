namespace Bulgarian_Apparel.Data.Models
{
    using Bulgarian_Apparel.Data.Models.Contracts;
    using System.Collections.Generic;
    using System;
    using Bulgarian_Apparel.Data.Models.Abstracts;
    using System.ComponentModel.DataAnnotations;

    public class Size : DataModel
    {
        public Size()
        {
            this.Items = new HashSet<Item>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

namespace Bulgarian_Apparel.Data.Models
{
    using Bulgarian_Apparel.Data.Models.Contracts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;
    using Bulgarian_Apparel.Data.Models.Abstracts;

    public class Color : DataModel
    {
        public Color()
        {
            this.Items = new HashSet<Item>();
        }

        [Required]
        public string Name { get; set; }

        //TO DO: Make the name of the color, its primary key

        public virtual ICollection<Item> Items { get; set; }
    }
}
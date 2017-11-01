namespace Bulgarian_Apparel.Data.Models
{
    using System.Collections.Generic;

    public class Size
    {
        public Size()
        {
            this.Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

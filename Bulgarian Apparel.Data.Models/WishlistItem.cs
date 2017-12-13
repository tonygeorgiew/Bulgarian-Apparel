using Bulgarian_Apparel.Data.Models.Abstracts;

namespace Bulgarian_Apparel.Data.Models
{
   public class WishlistItem : DataModel
    {
        public virtual Product Product { get; set; }

        public virtual User Customer { get; set; }

        public bool BeenPurchased { get; set; }
    }
}

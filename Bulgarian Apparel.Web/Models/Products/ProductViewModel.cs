using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Sizes { get; set; }
        public string Colors { get; set; }
    }
}
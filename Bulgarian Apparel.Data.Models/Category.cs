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
    public class Category : DataModel
    {
        [Required]
        public string Name { get; set; }

        public Guid SuperCategoryId { get; set; }

        public string SuperCategoryName { get; set; }
    }
}

using Layer.Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Concrete.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ProductFeature ProductFeature { get; set; }
    }
}

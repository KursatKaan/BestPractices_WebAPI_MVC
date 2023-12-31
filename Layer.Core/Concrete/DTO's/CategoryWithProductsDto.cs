using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Concrete.DTO_s
{
    public class CategoryWithProductsDto: CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}

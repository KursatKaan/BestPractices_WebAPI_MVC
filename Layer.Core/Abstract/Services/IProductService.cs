using Layer.Core.Abstract.DTO_s;
using Layer.Core.Concrete.DTO_s;
using Layer.Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Abstract.Services
{
    public interface IProductService:IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
    }
}

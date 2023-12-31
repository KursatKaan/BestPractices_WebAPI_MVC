using Layer.Core.Abstract.DTO_s;
using Layer.Core.Concrete.DTO_s;
using Layer.Core.Concrete.Entities;

namespace Layer.Core.Abstract.Services
{
    public interface ICategoryService:IService<Category>
    {
        public Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
    }
}

using AutoMapper;
using Layer.Core.Abstract.DTO_s;
using Layer.Core.Abstract.Repositories;
using Layer.Core.Abstract.Services;
using Layer.Core.Abstract.UnitOfWorks;
using Layer.Core.Concrete.DTO_s;
using Layer.Core.Concrete.Entities;

namespace Layer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);

            return CustomResponseDto<CategoryWithProductsDto>.Success(200,categoryDto);
        }
    }
}

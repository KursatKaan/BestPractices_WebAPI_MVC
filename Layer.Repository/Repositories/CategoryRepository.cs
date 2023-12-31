using Layer.Core.Abstract.Repositories;
using Layer.Core.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace Layer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _context.Categories.Include(x=>x.Products).Where(x=>x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}

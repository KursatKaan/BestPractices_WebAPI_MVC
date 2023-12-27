using Layer.Core.Abstract.Repositories;
using Layer.Core.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace Layer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            //Eager Loading
            return await _context.Products.Include(x=> x.Category).ToListAsync();
        }
    }
}

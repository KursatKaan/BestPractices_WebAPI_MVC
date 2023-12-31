using Layer.Core.Concrete.Entities;

namespace Layer.Core.Abstract.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategoryAsync();
    }
}

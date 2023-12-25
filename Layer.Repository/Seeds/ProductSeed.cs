using Layer.Core.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Layer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                ProductName = "Kalem 1",
                Price = 100,
                Stock = 20,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                ProductName = "Kalem 2",
                Price = 200,
                Stock = 20,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 3,
                CategoryId = 1,
                ProductName = "Kalem 3",
                Price = 300,
                Stock = 20,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 4,
                CategoryId = 2,
                ProductName = "Kitap 1",
                Price = 400,
                Stock = 10,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 5,
                CategoryId = 2,
                ProductName = "Kitap 2",
                Price = 300,
                Stock = 40,
                CreatedDate = DateTime.Now
            });
        }
    }
}

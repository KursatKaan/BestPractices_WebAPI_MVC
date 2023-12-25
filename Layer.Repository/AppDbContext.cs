using Layer.Core.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Layer.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Configurationlar uygulanır.
            base.OnModelCreating(modelBuilder);
        }
    }
}

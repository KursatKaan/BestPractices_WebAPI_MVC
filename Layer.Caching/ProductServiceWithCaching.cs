using AutoMapper;
using Layer.Core.Abstract.DTO_s;
using Layer.Core.Abstract.Repositories;
using Layer.Core.Abstract.Services;
using Layer.Core.Abstract.UnitOfWorks;
using Layer.Core.Concrete.DTO_s;
using Layer.Core.Concrete.Entities;
using Layer.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            //Eğer önbellekte ürünler yoksa, veritabanından çekip önbelleğe al
            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategoryAsync().Result); //Ürünleri ve kategorileri birlikte cachele
            }
        }

        //İşlemlerimizi kolaylaştırmak için önnbelleğe alma işlemini tek metotta belirtip aşağıdaki metotlarda bu metodu çağırıyoruz.
        public async Task CacheAllProductsAsync()
        {
            _memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync()); // Tüm ürünleri önbelleğe al
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity); // Yeni ürünü veritabanına ekle
            await _unitOfWork.CommitAsync(); // Veritabanı değişikliklerini kaydet
            await CacheAllProductsAsync(); // Tüm ürünleri önbelleğe al
            return entity; // Eklenen ürünü döndür

        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities); // Veritabanına yeni ürün koleksiyonunu ekle
            await _unitOfWork.CommitAsync(); // Veritabanı değişikliklerini kaydet
            await CacheAllProductsAsync(); // Tüm ürünleri önbelleğe al
            return entities; // Eklenen ürünleri döndürür (burada aslında gelen koleksiyonu geri döndürüyor)
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            // Tüm ürünleri önbellekten al, belirtilen koşula uyan ürün var mı kontrol et
            return Task.FromResult(_memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).Any()); 
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey)); // Tüm ürünleri önbellekten al
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id); // Belirli bir ID'ye sahip ürünü önbellekten al

            // Eğer ürün bulunamazsa NotFound hatası fırlat
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} ({id}) not found"); //404, NotFoundException hatasını fırlattık.
            }

            return Task.FromResult(product); // Bulunan ürünü döndür
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey); // Tüm ürünleri önbellekten al
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products); // // Ürünleri kategori DTO'ya dönüştür
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto)); // CustomResponseDto içinde döndür
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity); // Belirli bir ürünü kaldır
            await _unitOfWork.CommitAsync();// Veritabanı değişikliklerini kaydet
            await CacheAllProductsAsync(); // Tüm ürünleri önbelleğe al
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);// Belirli bir koleksiyondaki ürünleri kaldır
            await _unitOfWork.CommitAsync();// Veritabanı değişikliklerini kaydet
            await CacheAllProductsAsync(); // Tüm ürünleri önbelleğe al
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity); // Belirli bir ürünü güncelle
            await _unitOfWork.CommitAsync(); // Veritabanı değişikliklerini kaydet
            await CacheAllProductsAsync(); // Tüm ürünleri önbelleğe al
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            // Belirtilen koşula uyan ürünleri önbellekten al ve IQueryable olarak döndür
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }
    }
}

using AutoMapper;
using Layer.API.Filters;
using Layer.Core.Abstract.DTO_s;
using Layer.Core.Abstract.Services;
using Layer.Core.Concrete.DTO_s;
using Layer.Core.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Layer.API.Controllers
{
    [ServiceFilter(typeof(NotFoundFilter<Product>))]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;

        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        ///  GET: api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategoryAsync()
        {
            return CreateActionResult(await _productService.GetProductsWithCategoryAsync());
        }

        /// GET: api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok( CustomResponseDto<List<ProductDto>>.Success(200, productsDtos)); //Bu kullanımda sürekli sonucu Ok veya BadRequest olarak belirtmek gerekiyor.

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos)); //CreateActionResult metodu ile sonucu metoda bagladık.
        }

        
        /// GET: api/products/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        /// CREATE: api/products
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto)); //201: Created.
        }

        /// UPDATE: api/products
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204: No Content.
        }

        /// DELETE: api/products/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204: No Content.
        }
    }
}

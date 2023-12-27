using AutoMapper;
using Layer.Core.Abstract.DTO_s;
using Layer.Core.Abstract.Services;
using Layer.Core.Concrete.DTO_s;
using Layer.Core.Concrete.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Layer.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;

        private readonly IService<Product> _service;

        private readonly IProductService _productService;

        public ProductsController(IService<Product> service, IMapper mapper, IProductService productService)
        {
            _service = service;
            _mapper = mapper;
            _productService = productService;
        }

        ///  GET: api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory() 
        {
            return CreateActionResult(await _productService.GetProductsWithCategory());
        }

        /// GET: api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok( CustomResponseDto<List<ProductDto>>.Success(200, productsDtos)); //Bu kullanımda sürekli sonucu Ok veya BadRequest olarak belirtmek gerekiyor.

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos)); //CreateActionResult metodu ile sonucu metoda bagladık.
        }

        /// GET: api/products/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        /// CREATE: api/products
        [HttpPost()]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto)); //201: Created.
        }

        /// UPDATE: api/products
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204: No Content.
        }

        /// DELETE: api/products/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204: No Content.
        }
    }
}

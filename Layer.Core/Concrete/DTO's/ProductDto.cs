using Layer.Core.Abstract.DTO_s;

namespace Layer.Core.Concrete.DTO_s
{
    public class ProductDto : BaseDto
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}

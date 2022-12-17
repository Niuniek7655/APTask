using System.Collections.Generic;

namespace APTask.DTO
{
    public class CategoryWithProductsDto : CategoryDto
    {
        public ICollection<ProductDto> Products { get; set; }
    }
}

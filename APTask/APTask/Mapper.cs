using APTask.DTO;
using APTask.Entities;
using System.Collections.Generic;
using System.Linq;

namespace APTask
{
    public static class Mapper
    {
        internal static IEnumerable<ProductWithCategoryDto> Map(this IEnumerable<Product> products)
        {
            return products.Select(x => x.MapProductWithCategoryDto());
        }

        internal static IEnumerable<CategoryWithProductsDto> Map(this IEnumerable<Category> categories)
        {
            return categories.Select(x => x.MapCategoryWithProductsDto());
        }

        internal static ProductDto Map(this Product product)
        {
            return new ProductDto()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductCode = product.ProductCode,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive,
            };
        }

        internal static ProductWithCategoryDto MapProductWithCategoryDto(this Product product)
        {
            return new ProductWithCategoryDto()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductCode = product.ProductCode,
                CategoryId = product.CategoryId,
                Category = product.Category.Map(),
                IsActive = product.IsActive,
            };
        }

        internal static CategoryDto Map(this Category category)
        {
            return new CategoryDto()
            {
                CategoryName = category.CategoryName,
                IsActive = category.IsActive,
            };
        }

        internal static CategoryWithProductsDto MapCategoryWithProductsDto(this Category category)
        {
            var result = new CategoryWithProductsDto()
            {
                CategoryName = category.CategoryName,
                IsActive = category.IsActive,
                Products = new List<ProductDto>()
            };

            if (category.Products is not null)
            {
                result.Products = category.Products.Select(x => x.Map()).ToList();
            }
            return result;
        }
    }
}

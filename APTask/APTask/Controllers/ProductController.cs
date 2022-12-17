using APTask.Attributes;
using APTask.DAL;
using APTask.DTO;
using APTask.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace APTask.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly APTaskDbContext _dbContext;
        public ProductController(APTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = (await _dbContext.Products.Include(x => x.Category).ToListAsync()).Map();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _dbContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product.MapProductWithCategoryDto());
        }

        [HttpPost("User/{userId}")]
        public async Task<IActionResult> Post(Guid userId, [FromBody] ProductDto dto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return BadRequest($"User {userId} not exist in application");
            }

            var category = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == dto.CategoryId);
            if (category is null)
            {
                return BadRequest($"Category {dto.CategoryId} not exist in application");
            }

            bool isExistProductCode = await _dbContext.Products.AnyAsync(x => x.ProductCode.Equals(dto.ProductCode));
            if (isExistProductCode)
            {
                return BadRequest($"Product code {dto.ProductCode} must be unique");
            }

            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                ProductName = dto.ProductName,
                Description = dto.Description,
                ProductCode = dto.ProductCode,
                Category = category,
                IsActive = dto.IsActive,
                CreatingUserId = userId,
                CreateDate = DateTime.UtcNow,
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return Accepted(product.Id);
        }

        [HttpPut("{id}/User/{userId}")]
        public async Task<IActionResult> Put(Guid id, Guid userId, [FromBody] ProductDto dto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return BadRequest($"User {userId} not exist in application");
            }

            var category = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == dto.CategoryId);
            if (category is null)
            {
                return BadRequest($"Category {dto.CategoryId} not exist in application");
            }

            var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                return BadRequest($"Product {id} not exist in application");
            }

            bool isExistProductCode = await _dbContext.Products.AnyAsync(x => x.ProductCode.Equals(dto.ProductCode));
            if (isExistProductCode)
            {
                return BadRequest($"Product code {dto.ProductCode} must be unique");
            }

            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.ProductCode = dto.ProductCode;
            product.Category = category;
            product.IsActive = dto.IsActive;
            product.ModifyingUserId = userId;
            product.ModificationDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return Accepted(product.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                return BadRequest($"Product {id} not exist in application");
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return Accepted(product.Id);
        }
    }
}

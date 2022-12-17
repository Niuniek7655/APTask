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
    public class CategoryController : ControllerBase
    {
        private readonly APTaskDbContext _dbContext;
        public CategoryController(APTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = (await _dbContext.Categories.Include(x => x.Products).ToListAsync()).Map();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await _dbContext.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category.MapCategoryWithProductsDto());
        }

        [HttpPost("User/{userId}")]
        public async Task<IActionResult> Post(Guid userId, [FromBody] CategoryDto dto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return BadRequest($"User {userId} not exist in application");
            }

            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                CategoryName = dto.CategoryName,
                IsActive = dto.IsActive,
                CreatingUserId = userId,
                CreateDate = DateTime.UtcNow,
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return Accepted(category.Id);
        }

        [HttpPut("{id}/User/{userId}")]
        public async Task<IActionResult> Put(Guid id, Guid userId, [FromBody] CategoryDto dto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return BadRequest($"User {userId} not exist in application");
            }

            var category = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                return BadRequest($"Category {id} not exist in application");
            }

            category.CategoryName = dto.CategoryName;
            category.IsActive = dto.IsActive;
            category.ModifyingUserId = userId;
            category.ModificationDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return Accepted(category.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                return BadRequest($"Product {id} not exist in application");
            }

            if(category.Products is not null && category.Products.Count > 0)
            {
                return BadRequest($"Category {id} contain existing products {category.Products.Count}");
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return Accepted(category.Id);
        }
    }
}

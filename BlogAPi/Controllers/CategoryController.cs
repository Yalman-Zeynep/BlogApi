using BlogAPi.Data;
using BlogAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BlogContext Context;

        public CategoryController(BlogContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            Context.Categories.Add(entity: category);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await Context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
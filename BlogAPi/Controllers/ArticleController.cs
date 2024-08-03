using BlogAPi.Data;
using BlogAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly BlogContext Context;
        public ArticleController(BlogContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            Context.Articles.Add(article);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, article);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await Context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();

            }
            return Ok(article);
        }
    }   
}

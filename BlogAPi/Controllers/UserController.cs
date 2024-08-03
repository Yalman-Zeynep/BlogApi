using BlogAPi.Data;
using BlogAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BlogContext Context;

        public UserController(BlogContext context)
        {
            Context = context;
        }


        [HttpPost]
        public async Task<ActionResult<UserController>> CreateUser(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}

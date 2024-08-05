using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace BlogProject.Models
{
    public class BlogContext: IdentityDbContext<ApplicationUser>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Auther> Auther { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Category> Category { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Blog> Blog { get; set; }
    }
}

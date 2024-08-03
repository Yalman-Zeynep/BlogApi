namespace BlogAPi.Data
{

    using BlogAPi.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;


    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}

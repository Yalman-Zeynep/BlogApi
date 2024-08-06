using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json;

namespace BlogProject.Models
    
    
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
       
        //public ICollection<Blog>? Blogs { get; set; }
    }
}

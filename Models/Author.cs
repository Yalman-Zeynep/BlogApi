using System.ComponentModel.DataAnnotations;
using System.Text.Json; 

namespace BlogProject.Models
{
    public class Author
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Surname { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        //public ICollection<Blog>? Blogs { get; set; }

      

    }
}

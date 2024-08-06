using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BlogProject.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Subtitle { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Content { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ImagePath { get; set; }
        public bool isPublish { get; set; }

       // public int AuthorId { get; set; }
        //public required Author Author { get; set; }

        //public int CategoryId { get; set; }
        //public required Category Category { get; set; }
       
       

      
    }
}

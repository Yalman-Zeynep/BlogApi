namespace BlogProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string? Title { get; set; }    
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }
        public bool isPublish { get; set; }
        public required Author Author { get; set; }
        public int AuthorId { get; set; }   

        public required Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}

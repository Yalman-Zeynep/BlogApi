namespace BlogProject.Models
{
    public class Author
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }

       
        public ICollection<Blog>? Blog { get; set; }

    }
}

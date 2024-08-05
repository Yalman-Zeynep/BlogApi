﻿namespace BlogProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string? Title { get; set; }    
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }
        public bool isPublish { get; set; }
        public Auther? Auther { get; set; }
        public int AuthorId { get; set; }   

        public Category? Category { get; set; }
        public int CategoryId { get; set; }

    }
}
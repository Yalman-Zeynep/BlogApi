using Microsoft.AspNetCore.Identity;

namespace BlogProject.Models
{


    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}

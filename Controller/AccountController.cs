using BlogProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Dependency Injection kullanarak UserManager ve SignInManager'i alıyoruz
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Kullanıcı kayıt işlemi için HTTP POST metodu
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };

                // Kullanıcı oluşturma işlemi
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User registered successfully" });
                }

                // Eğer hata varsa, hataları döndür
                return BadRequest(result.Errors);
            }

            // Model doğrulaması geçmezse
            return BadRequest(ModelState);
        }

        // Kullanıcı giriş işlemi için HTTP POST metodu
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı giriş işlemi
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Login successful" });
                }

                // Giriş başarısız olursa
                return Unauthorized(new { Message = "Invalid login attempt" });
            }

            // Model doğrulaması geçmezse
            return BadRequest(ModelState);
        }

        // Kullanıcı çıkış işlemi için HTTP POST metodu
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Logout successful" });
        }
    }


    // Kullanıcı kayıt modeli
    public class RegisterModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    // Kullanıcı giriş modeli
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

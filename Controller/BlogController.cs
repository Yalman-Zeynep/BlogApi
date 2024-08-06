// Proje model sınıflarını ve gerekli ASP.NET Core kütüphanelerini ekliyoruz
using BlogProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Controller
{
    // API'nin URL rotasını belirler. Burada, controller adı otomatik olarak kullanılır.
    [Route("api/[controller]")]
    // API controller'ı olarak işaretler, model doğrulama ve JSON dönüşüm gibi işlemleri otomatik olarak yapar.
    [ApiController]
    public class BlogController : ControllerBase
    {
        // BlogContext veritabanı bağlamını tanımlıyoruz
        private readonly BlogContext _context;

        // Constructor, veritabanı bağlamını dependency injection ile alır
        public BlogController(BlogContext context)
        {
            _context = context;
        }

        // Tüm blog yazılarını listelemek için GET isteği
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            // Veritabanından tüm blog yazılarını ve ilişkili yazar ve kategori bilgilerini alır ve liste olarak döner
            return await _context.Blogs
               // .Include(b => b.Author)   // Blog yazılarıyla ilişkili yazarları da içerir
                //.Include(b => b.Category) // Blog yazılarıyla ilişkili kategorileri de içerir
                .ToListAsync();
        }

        // Belirli bir blog yazısını ID ile almak için GET isteği
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            // Veritabanında verilen ID'ye sahip blog yazısını ve ilişkili yazar ve kategori bilgilerini bulur
            var blog = await _context.Blogs
                //.Include(b => b.Author)
                //.Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            // Blog yazısı bulunamazsa 404 Not Found döner
            if (blog == null)
            {
                return NotFound();
            }

            // Blog yazısı bulunursa, blog objesini döner
            return blog;
        }

        // Yeni bir blog yazısı eklemek için POST isteği
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            // Yeni blog objesini veritabanına ekler
            _context.Blogs.Add(blog);
            // Değişiklikleri veritabanına kaydeder
            await _context.SaveChangesAsync();

            // Yeni blog yazısı oluşturulduktan sonra, oluşturulan blog yazısını ve onun URL'sini döner
            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
        }

        // Varolan bir blog yazısını güncellemek için PUT isteği
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            // ID'ler eşleşmiyorsa 400 Bad Request döner
            if (id != blog.Id)
            {
                return BadRequest();
            }

            // Blog objesinin durumunu değiştirilmiş olarak işaretler
            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                // Veritabanındaki değişiklikleri kaydeder
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Eğer blog yazısı bulunamazsa 404 Not Found döner
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Diğer hatalar için istisnayı yeniden fırlatır
                    throw;
                }
            }

            // Başarılı güncelleme durumunda 204 No Content döner
            return NoContent();
        }

        // Belirli bir blog yazısını ID ile silmek için DELETE isteği
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            // Veritabanında verilen ID'ye sahip blog yazısını bulur
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                // Blog yazısı bulunamazsa 404 Not Found döner
                return NotFound();
            }

            // Blog objesini veritabanından kaldırır
            _context.Blogs.Remove(blog);
            // Değişiklikleri veritabanına kaydeder
            await _context.SaveChangesAsync();

            // Başarılı silme durumunda 204 No Content döner
            return NoContent();
        }

        // Verilen ID ile blog yazısının var olup olmadığını kontrol eder
        private bool BlogExists(int id)
        {
            // Veritabanında verilen ID'ye sahip bir blog yazısının var olup olmadığını kontrol eder
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}


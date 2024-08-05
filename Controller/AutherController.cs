// BlogProject.Models ve Microsoft kütüphanelerini ekliyoruz
using BlogProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// API için rotayı tanımlıyoruz ve ApiController özelliğini belirtiyoruz
namespace BlogProject.Controller
{
    [Route("api/[controller]")] // API'nin rotasını belirler. Burada, controller adı otomatik olarak kullanılır.
    [ApiController] // API controller'ı olarak işaretler, model doğrulama ve JSON dönüşüm gibi işlemleri otomatik olarak yapar.
    public class AutherController : ControllerBase
    {
        // BlogContext veritabanı bağlamını tanımlıyoruz
        private readonly BlogContext _context;

        // Constructor, veritabanı bağlamını enjeksiyonla alır
        public AutherController(BlogContext context)
        {
            _context = context;
        }

        // Tüm yazarları listelemek için GET isteği
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auther>>> GetAuthors()
        {
            // Veritabanından tüm yazarları alır ve liste olarak döner
            return await _context.Auther.ToListAsync();
        }

        // Belirli bir yazarı ID ile almak için GET isteği
        [HttpGet("{id}")]
        public async Task<ActionResult<Auther>> GetAuthor(int id)
        {
            // Veritabanında verilen ID'ye sahip yazarı bulur
            var author = await _context.Auther.FindAsync(id);

            // Yazar bulunamazsa 404 Not Found döner
            if (author == null)
            {
                return NotFound();
            }

            // Yazar bulunursa, yazar objesini döner
            return author;
        }

        // Yeni bir yazar eklemek için POST isteği
        [HttpPost]
        public async Task<ActionResult<Auther>> PostAuthor(Auther auther)
        {
            // Yeni yazar objesini veritabanına ekler
            _context.Auther.Add(auther);
            await _context.SaveChangesAsync();

            // Yeni yazar oluşturulduktan sonra, oluşturulan yazarı ve onun URL'sini döner
            return CreatedAtAction(nameof(GetAuthor), new { id = auther.Id }, auther);
        }

        // Varolan bir yazarı güncellemek için PUT isteği
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Auther author)
        {
            // ID'ler eşleşmiyorsa 400 Bad Request döner
            if (id != author.Id)
            {
                return BadRequest();
            }

            // Yazar objesinin durumunu değiştirilmiş olarak işaretler
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                // Veritabanındaki değişiklikleri kaydeder
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Eğer yazar bulunamazsa 404 Not Found döner
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Diğer hatalar için istisnayı yeniden fırlatır
                }
            }

            // Başarılı güncelleme durumunda 204 No Content döner
            return NoContent();
        }

        // Belirli bir yazarı ID ile silmek için DELETE isteği
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            // Veritabanında verilen ID'ye sahip yazarı bulur
            var author = await _context.Auther.FindAsync(id);
            if (author == null)
            {
                // Yazar bulunamazsa 404 Not Found döner
                return NotFound();
            }

            // Yazar objesini veritabanından kaldırır
            _context.Auther.Remove(author);
            await _context.SaveChangesAsync();

            // Başarılı silme durumunda 204 No Content döner
            return NoContent();
        }

        // Verilen ID ile yazarın var olup olmadığını kontrol eder
        private bool AuthorExists(int id)
        {
            return _context.Auther.Any(e => e.Id == id);
        }
    }
}

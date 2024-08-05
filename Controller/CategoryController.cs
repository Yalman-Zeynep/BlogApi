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
    public class CategoryController : ControllerBase
    {
        // BlogContext veritabanı bağlamını tanımlıyoruz
        private readonly BlogContext _context;

        // Constructor, veritabanı bağlamını enjeksiyonla alır
        public CategoryController(BlogContext context)
        {
            _context = context;
        }

        // Tüm kategorileri listelemek için GET isteği
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            // Veritabanından tüm kategorileri alır ve liste olarak döner
            return await _context.Category.ToListAsync();
        }

        // Belirli bir kategoriyi ID ile almak için GET isteği
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            // Veritabanında verilen ID'ye sahip kategoriyi bulur
            var category = await _context.Category.FindAsync(id);

            // Kategori bulunamazsa 404 Not Found döner
            if (category == null)
            {
                return NotFound();
            }

            // Kategori bulunursa, kategori objesini döner
            return category;
        }

        // Yeni bir kategori eklemek için POST isteği
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            // Yeni kategori objesini veritabanına ekler
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            // Yeni kategori oluşturulduktan sonra, oluşturulan kategoriyi ve onun URL'sini döner
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // Varolan bir kategoriyi güncellemek için PUT isteği
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            // ID'ler eşleşmiyorsa 400 Bad Request döner
            if (id != category.Id)
            {
                return BadRequest();
            }

            // Kategori objesinin durumunu değiştirilmiş olarak işaretler
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                // Veritabanındaki değişiklikleri kaydeder
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Eğer kategori bulunamazsa 404 Not Found döner
                if (!CategoryExists(id))
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

        // Belirli bir kategoriyi ID ile silmek için DELETE isteği
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // Veritabanında verilen ID'ye sahip kategoriyi bulur
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                // Kategori bulunamazsa 404 Not Found döner
                return NotFound();
            }

            // Kategori objesini veritabanından kaldırır
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            // Başarılı silme durumunda 204 No Content döner
            return NoContent();
        }

        // Verilen ID ile kategorinin var olup olmadığını kontrol eder
        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}



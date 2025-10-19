using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Server.Data;
using WebAPI.Server.Model;

namespace WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Dependency Injection ile AppDbContext'i al
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Tüm ürünleri listele
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Id'ye göre tek bir ürün getir
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                // Ürün bulunamazsa 404 Not Found döndür
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            /*
             * Model validasyonu (Required, StringLength) [ApiController] 
             * özelliği sayesinde bu metod çalışmadan önce otomatik olarak yapılır.
             * Model geçersizse framework otomatik 400 Dönecektir.
            */

            // Yeni bir ürün oluştur
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // 201 Created cevabı ve oluşturulan kaynağın adresini (Location header) döndür
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            // URL'deki id ile gövdedeki (body) product.Id eşleşmiyorsa 400 Bad Request döndür
            if (id != product.Id)
            {
                return BadRequest();
            }

            /*
             * Model validasyonu (Required, StringLength) [ApiController] 
             * özelliği sayesinde bu metod çalışmadan önce otomatik olarak yapılır.
            */

            // Entity'nin durumunu Modified olarak işaretle
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                // Değişiklikleri kaydet
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Kayıt sırasında bir çakışma olursa (örn. aynı anda silinirse)
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Başarılı güncelleme sonrası 204 No Content döndür
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Ürünü bul
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                // Bulunamazsa 404 döndür
                return NotFound();
            }

            // Ürünü sil
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // Başarılı silme sonrası 204 No Content döndür
            return NoContent();
        }

        // Yardımcı metot: Ürünün var olup olmadığını kontrol et
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
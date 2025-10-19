using System.ComponentModel.DataAnnotations;

namespace WebAPI.Server.Model
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(250, ErrorMessage ="Ürün adı en çok 100 karakter olmalı!")]
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        //category ile ilişkisi olmasın
    }
}

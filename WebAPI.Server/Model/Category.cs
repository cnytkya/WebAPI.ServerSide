using System.ComponentModel.DataAnnotations;

namespace WebAPI.Server.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)] //name alanı max 100 karakter 
        public string Name { get; set; }
        //public string Description { get; set; }
    }
}

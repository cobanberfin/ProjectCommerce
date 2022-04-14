using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60,MinimumLength =5,ErrorMessage ="ürün ismi min 10 karakter max 60 karakter olmalıdır")]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
         [Required(ErrorMessage ="Fiyat Belirtiniz")]
         [Range(1,1000)]
         public decimal? Price { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "ürün acıklaması min 20 karakter max 100 karakter olmalıdır")]
        public string Description { get; set; }
    }
}

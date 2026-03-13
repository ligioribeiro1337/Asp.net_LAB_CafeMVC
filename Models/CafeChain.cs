using System.ComponentModel.DataAnnotations;
namespace CafeMVC.Models
{
    public class CafeChain
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [MaxLength(200)]
        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите тип кухни")]
        [MaxLength(100)]
        [Display(Name = "Тип кухни")]
        public string CuisineType { get; set; } = string.Empty;
        [MaxLength(300)]
        [Display(Name = "Регионы")]
        public string Regions { get; set; } = string.Empty;
        [MaxLength(500)]
        [Display(Name = "Меню")]
        public string Menu { get; set; } = string.Empty;
        [Display(Name = "Год основания")]
        [Range(1800, 2100, ErrorMessage = "Год от 1800 до 2100")]
        public int FoundedYear { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}

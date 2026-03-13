using System.ComponentModel.DataAnnotations;
namespace CafeMVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите номер заказа")]
        [MaxLength(50)]
        [Display(Name = "Номер заказа")]
        public string OrderNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите блюда")]
        [MaxLength(500)]
        [Display(Name = "Блюда")]
        public string Dishes { get; set; } = string.Empty;
        [Display(Name = "Время заказа")]
        public DateTime OrderTime { get; set; } = DateTime.UtcNow;
        [Required]
        [MaxLength(50)]
        [Display(Name = "Статус")]
        public string Status { get; set; } = "Новый";
        [Display(Name = "Сеть кафе")]
        public int CafeChainId { get; set; }
        [Display(Name = "Сеть кафе")]
        public CafeChain? CafeChain { get; set; }
    }
}

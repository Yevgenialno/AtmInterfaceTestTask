using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Models
{
    public class CardNumberViewModel
    {
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        public string CardNumber { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Models
{
    public class CardNumberViewModel
    {
        [Required]
        [MaxLength(19)]
        [MinLength(19)]
        public string CardNumber { get; set; }
    }
}

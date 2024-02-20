using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(4)]
        [MinLength(4)]
        public string Pin { get; set; }
    }
}

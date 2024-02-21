using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Models.Login
{
    public class PinViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(4)]
        [MinLength(4)]
        public string Pin { get; set; }
    }
}

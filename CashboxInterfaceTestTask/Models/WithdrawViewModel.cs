using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Models
{
    public class WithdrawViewModel
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Withdraw amount must be positive")]
        public decimal Amount { get; set; }
    }
}

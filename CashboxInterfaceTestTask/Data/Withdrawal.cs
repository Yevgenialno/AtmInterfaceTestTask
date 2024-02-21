using CashboxInterfaceTestTask.Models;
using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Data
{
    public class Withdrawal
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public decimal Amount { get; set; }

        public DateTime Time {  get; set; } = DateTime.Now;
    }
}

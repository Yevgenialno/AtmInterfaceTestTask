using CashboxInterfaceTestTask.Models;
using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Data
{
    public class BalanceView
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;
    }
}

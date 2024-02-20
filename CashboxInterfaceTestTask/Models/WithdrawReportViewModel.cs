namespace CashboxInterfaceTestTask.Models
{
    public class WithdrawReportViewModel
    {
        public string CardNumber { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        public decimal Rest { get; set; }
    }
}

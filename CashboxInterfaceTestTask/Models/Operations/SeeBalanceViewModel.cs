namespace CashboxInterfaceTestTask.Models.Operations
{
    public class SeeBalanceViewModel
    {
        public string CardNumber { get; set; }

        public decimal Balance { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}

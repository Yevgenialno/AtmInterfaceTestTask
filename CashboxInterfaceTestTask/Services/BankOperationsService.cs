using CashboxInterfaceTestTask.Data;
using CashboxInterfaceTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CashboxInterfaceTestTask.Services
{
    public class BankOperationsService : IBankOperationsService
    {
        public BankOperationsService() { }

        public async Task<bool> WithdrawFunds(ApplicationDbContext context, ApplicationUser user, decimal amount)
        {
            if (user.Balance > amount)
            {
                user.Balance -= amount;
                var withdrawal = new Withdrawal()
                {
                    User = user,
                    Amount = amount,
                };
                context.Withdrawals.Add(withdrawal);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using CashboxInterfaceTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CashboxInterfaceTestTask.Services
{
    public class BankOperationsService : IBankOperationsService
    {
        public BankOperationsService() { }

        public async Task<bool> WithdrawFunds(DbContext context, ApplicationUser user, decimal amount)
        {
            if (user.Balance > amount)
            {
                user.Balance -= amount;
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

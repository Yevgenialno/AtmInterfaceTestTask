using CashboxInterfaceTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CashboxInterfaceTestTask.Services
{
    public interface IBankOperationsService
    {
        public Task<bool> WithdrawFunds(DbContext context, ApplicationUser user, decimal amount);
    }
}
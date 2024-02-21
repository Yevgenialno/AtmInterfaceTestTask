using CashboxInterfaceTestTask.Data;
using CashboxInterfaceTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CashboxInterfaceTestTask.Services
{
    public interface IBankOperationsService
    {
        public Task<bool> WithdrawFunds(ApplicationDbContext context, ApplicationUser user, decimal amount);
    }
}
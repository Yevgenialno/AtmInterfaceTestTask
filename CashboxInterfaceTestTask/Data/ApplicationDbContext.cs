using CashboxInterfaceTestTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CashboxInterfaceTestTask.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { 
        }

        public new DbSet<ApplicationUser> Users {  get; set; }

        public DbSet<Withdrawal> Withdrawals { get; set; }

        public DbSet<BalanceView> BalanceViews { get; set; }
    }
}

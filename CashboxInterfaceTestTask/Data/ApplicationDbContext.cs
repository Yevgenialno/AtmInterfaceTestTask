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

        public DbSet<ApplicationUser> Users {  get; set; }
    }
}

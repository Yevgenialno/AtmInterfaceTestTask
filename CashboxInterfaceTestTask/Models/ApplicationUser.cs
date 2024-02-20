using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CashboxInterfaceTestTask.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string CardNumber { get; set; }

        public decimal Balance { get; set; } = 0;
    }
}

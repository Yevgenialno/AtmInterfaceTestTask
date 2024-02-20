using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CashboxInterfaceTestTask.Data;
using CashboxInterfaceTestTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CashboxInterfaceTestTask.Services;

namespace CashboxInterfaceTestTask.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBankOperationsService _operations;
        private readonly UserManager<ApplicationUser> _userManager;

        public OperationsController(ApplicationDbContext context, IBankOperationsService bankOperationsService, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _operations = bankOperationsService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Withdraw()
        {
            return View(new WithdrawViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Withdraw(WithdrawViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            bool result = await _operations.WithdrawFunds(_context, user, model.Amount);
            if (result)
            {
                var repotModel = new WithdrawReportViewModel()
                {
                    CardNumber = user.CardNumber,
                    DateTime = DateTime.Now,
                    Amount = model.Amount,
                    Rest = user.Balance,
                };
                return View("OperationReport", repotModel);
            }

            var errorModel = new WithdrawErrorViewModel() { ErrorMessage = "Invalid amount to withdraw", };
            return View("WithdrawError", errorModel);
        }
    }
}

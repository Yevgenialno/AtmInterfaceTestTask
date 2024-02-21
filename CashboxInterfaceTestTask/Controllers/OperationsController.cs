using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CashboxInterfaceTestTask.Data;
using CashboxInterfaceTestTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CashboxInterfaceTestTask.Services;

namespace CashboxInterfaceTestTask.Controllers
{
    public class OperationsController(ApplicationDbContext context, IBankOperationsService bankOperationsService, UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IBankOperationsService _operations = bankOperationsService;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

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
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                bool result = await _operations.WithdrawFunds(_context, user, model.Amount);
                if (result)
                {
                    var repotModel = new WithdrawReportViewModel()
                    {
                        CardNumber = user.CardNumber,
                        Amount = model.Amount,
                        Rest = user.Balance,
                    };
                    return View("WithdrawReport", repotModel);
                }

                var errorModel = new WithdrawErrorViewModel() { ErrorMessage = "Invalid amount to withdraw", };
                return View("WithdrawError", errorModel);
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SeeBalance()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new SeeBalanceViewModel() { Balance = user.Balance, CardNumber = user.CardNumber };
            var balanceView = new BalanceView()
            {
                User = user,
            };
            _context.BalanceViews.Add(balanceView);
            await _context.SaveChangesAsync();
            return View(model);
        }
    }
}

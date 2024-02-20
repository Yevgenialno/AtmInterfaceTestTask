using CashboxInterfaceTestTask.Data;
using CashboxInterfaceTestTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashboxInterfaceTestTask.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Console.WriteLine("started registration");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    CardNumber = model.CardNumber,
                    UserName = model.CardNumber,
                };

                var result = await _userManager.CreateAsync(user, model.Pin);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EnterCardNumber()
        {
            return View(new CardNumberViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnterCardNumber(CardNumberViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Users.SingleAsync(u => u.CardNumber == model.CardNumber);
                }
                catch (InvalidOperationException ex)
                {
                    var errorModel = new InvalidLoginViewModel() { ErrorMessage = "This card number does not exist" };
                    return View("InvalidLogin", errorModel);
                }

                TempData["CardNumber"] = model.CardNumber;
                return View("EnterPin");
            }

            return View(new CardNumberViewModel());
        }

        [HttpGet]
        public IActionResult EnterPin()
        {
            return View(new PinViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnterPin(PinViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("sign user" + TempData["CardNumber"] as string);
                var user = _context.Users.Single(u => u.CardNumber == TempData["CardNumber"] as string);
                var result = await _signInManager.PasswordSignInAsync(user, model.Pin, false, false);
                if (result.Succeeded)
                {
                    Console.WriteLine("signed in " + user.CardNumber);
                    return RedirectToAction("Index", "Operations");
                }
                else
                {
                    var errorModel = new InvalidLoginViewModel() { ErrorMessage = "Pin is incorrect" };
                    return View("InvalidLogin", errorModel);
                }

            }
            return View(new PinViewModel());
        }
    }
}

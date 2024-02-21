using CashboxInterfaceTestTask.Data;
using CashboxInterfaceTestTask.Models;
using CashboxInterfaceTestTask.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CashboxInterfaceTestTask.Models.Operations;

namespace CashboxInterfaceTestTask.Controllers
{
    public class AccountController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext context) : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ApplicationDbContext _context = context;

        // uncomment to fill database with test users
        /*[HttpGet]
        public async Task<IActionResult> RegisterAll()
        {
            foreach (var number in Enumerable.Range(0, 5))
            {
                var user = new ApplicationUser
                {
                    CardNumber = new string(number.ToString()[0], 16),
                    UserName = new string(number.ToString()[0], 16),
                    Balance = Random.Shared.Next(100, 5000),
                };

                var result = await _userManager.CreateAsync(user, new string(number.ToString()[0], 4));
            }

            return RedirectToAction("EnterCardNumber");
        }*/

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
                string cardNumber = model.CardNumber.Replace("-", string.Empty);
                try
                {
                    await _context.Users.SingleAsync(u => u.CardNumber == cardNumber);
                }
                catch (InvalidOperationException)
                {
                    var errorModel = new InvalidLoginViewModel() { ErrorMessage = "This card number does not exist" };
                    return View("InvalidLogin", errorModel);
                }

                TempData["CardNumber"] = cardNumber;
                return RedirectToAction("EnterPin");
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
                ApplicationUser user;
                try
                {
                    user = _context.Users.Single(u => u.CardNumber == TempData.Peek("CardNumber") as string);
                }
                catch (InvalidOperationException)
                {
                    var errorModel = new InvalidLoginViewModel() { ErrorMessage = "This card number does not exist" };
                    return View("InvalidLogin", errorModel);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Pin, false, true);
                if (result.Succeeded)
                {
                    Console.WriteLine("signed in " + user.CardNumber);
                    return RedirectToAction("Index", "Operations");
                }

                if (result.IsLockedOut)
                {
                    var errorModel = new InvalidLoginViewModel() { ErrorMessage = "Your card is blocked" };
                    return View("InvalidLogin", errorModel);
                }
                else
                {
                    var errorModel = new InvalidLoginViewModel() { ErrorMessage = "Pin is incorrect" };
                    return View("InvalidLogin", errorModel);
                }
            }

            return View(new PinViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("EnterCardNumber");
        }
    }
}

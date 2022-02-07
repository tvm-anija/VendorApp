using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VendorWeb.Models;
using VendorWeb.Repository.IRepository;

namespace VendorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IResetRepository _resetRepository;
        private readonly IDepositRepository _depositRepository;
        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository,IUserRepository userRepository,IResetRepository resetRepository, IDepositRepository depositRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _resetRepository = resetRepository;
            _depositRepository = depositRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login obj)
        {
            User userObj = await _userRepository.LoginAsync(SD.userPath + "authenticate/", obj);
            if(userObj.Token == null)
            {
                return View();
            }

            HttpContext.Session.SetString("JWToken", userObj.Token);
            HttpContext.Session.SetString("Role", userObj.Role.ToString());
            HttpContext.Session.SetString("UserID", userObj.UserId.ToString());
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            User result = await _userRepository.RegisterAsync(SD.userPath + "register/", obj);
            if (result.UserId == 0)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout(User obj)
        {
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Settings()
        {
            int userId= Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            User user = await _userRepository.GetAsync(SD.userPath, userId, HttpContext.Session.GetString("JWToken")?.ToString());
            if (user == null)
            {
                return View();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(string submitButton, User obj)
        {
            obj.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "update":
                        await _userRepository.UpdateAsync(SD.userPath + obj.UserId, obj, HttpContext.Session.GetString("JWToken")?.ToString());
                        return RedirectToAction(nameof(Index));
                    case "delete":
                        await _userRepository.DeleteAsync(SD.userPath, obj.UserId, HttpContext.Session.GetString("JWToken")?.ToString());
                        return RedirectToAction(nameof(Index));
                    case "reset":
                        await _resetRepository.ResetAsync(SD.resetPath+obj.UserId,obj.UserId, HttpContext.Session.GetString("JWToken")?.ToString());
                        return RedirectToAction(nameof(Index));
                    default:
                        return View(obj);
                }
               
                
               
            }
            else
            {
                return View(obj);
            }

        }
        public async Task<IActionResult> Deposit()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(DepositDto obj)
        {
            obj.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

                        await _depositRepository.UpdateAsync(SD.deposit+obj.UserId, obj, HttpContext.Session.GetString("JWToken")?.ToString());
                        return RedirectToAction(nameof(Index));
        }

    }
}

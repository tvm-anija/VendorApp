using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VendorWeb.Models;
using VendorWeb.Repository.IRepository;

namespace VendorWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ProductController(IProductRepository productRepository,IUserRepository userRepository,IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public IActionResult Index()
        {
            return View(new Product() { });
        }
        public IActionResult UnAutherized()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var role = HttpContext.Session.GetString("Role")?.ToString();
            if (role == "buyer")
            {
                return View(nameof(UnAutherized));
            }
            Product product = new Product();
            if (id == null)
            {
                //this will be true for insert
                return View(product);
            }

            //update
            product = await _productRepository.GetAsync(SD.productPath, id.GetValueOrDefault(),HttpContext.Session.GetString("JWToken")?.ToString());
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> GetAllProducts()
        {
            return Json(new { data = await _productRepository.GetAllAsync(SD.productPath, HttpContext.Session.GetString("JWToken")?.ToString()) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _productRepository.DeleteAsync(SD.productPath,id, HttpContext.Session.GetString("JWToken")?.ToString());
            if (status)
            {
                return Json(new { success = true,message="Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Product obj)
        {
            obj.SellerId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _productRepository.CreateAsync(SD.productPath,obj, HttpContext.Session.GetString("JWToken")?.ToString());
                }
                else
                {
                    await _productRepository.UpdateAsync(SD.productPath+obj.Id,obj, HttpContext.Session.GetString("JWToken")?.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }

        }
        public IActionResult Buy(int? id)
        {
            return View(new Product() { });
        }

        public async Task<IActionResult> ShoppingCart(int? id)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            if (id == null)
            {
                //this will be true for insert
                return View(shoppingCart);
            }

            //update
           Product product = await _productRepository.GetAsync(SD.productPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken")?.ToString());
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                shoppingCart.ProductName = product.ProductName;
                shoppingCart.Cost = product.Cost;
                shoppingCart.AmountAvailable = product.AmountAvailable;

            }
            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShoppingCart(ShoppingCart obj)
        {
            obj.ApplicationUserId = HttpContext.Session.GetString("UserID");
            obj.MenuItemId = obj.Id;
            obj.Id = 0;
            if (ModelState.IsValid)
            {

                 ShoppingCart shoppingCart =   await _shoppingCartRepository.CreateShoppingCart(SD.shoppingCart, obj, HttpContext.Session.GetString("JWToken")?.ToString());
                return RedirectToAction(nameof(CartSummary));
            }
            else
            {
                return View(obj);
            }

        }
        public async Task<IActionResult> CartSummary(ShoppingCart buyDto)
        {
            int total = 0;
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            buyDto = await _shoppingCartRepository.GetAsync(SD.shoppingCart, userId, HttpContext.Session.GetString("JWToken")?.ToString());
            foreach(var item in buyDto.products)
            {
                foreach(var cart in buyDto.shoppingCarts)
                {
                    if (item.Id == cart.MenuItemId)
                    {
                        total = total + (item.Cost*cart.Count);
                    }
                }
            }
            buyDto.Cost = total;
            buyDto.AmountAvailable = buyDto.User.Deposit - total;
            return View(buyDto);
        }
        public async Task<IActionResult> GetCartSummary()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            return Json(new { data = await _shoppingCartRepository.GetAsync(SD.shoppingCart, userId, HttpContext.Session.GetString("JWToken")?.ToString()) });
        }
    }
}

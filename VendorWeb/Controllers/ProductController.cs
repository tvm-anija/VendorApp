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

        public async Task<IActionResult> Upsert(int? id)
        {
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShoppingCart(ShoppingCart obj)
        {
            obj.SellerId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _shoppingCartRepository.CreateAsync(SD.shoppingCart, obj, HttpContext.Session.GetString("JWToken")?.ToString());
                }
                else
                {
                    await _shoppingCartRepository.UpdateAsync(SD.shoppingCart + obj.Id, obj, HttpContext.Session.GetString("JWToken")?.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }

        }
    }
}

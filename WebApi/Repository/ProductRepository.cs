using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    /// <summary>
    /// The product repository
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The Product Repository constructor
        /// </summary>
        /// <param name="db">The DB context</param>
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// To Create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>bool</returns>
        public bool CreateProduct(Product product)
        {
            _db.products.Add(product);
            return Save();
        }

        /// <summary>
        /// To delete a product
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        public bool DeleteProduct(Product product)
        {
            _db.products.Remove(product);
            return Save();
        }

        /// <summary>
        /// Get individual product by id
        /// </summary>
        /// <param name="id">The product id</param>
        /// <returns>The product object</returns>
        public Product GetProduct(int id)
        {
            return _db.products.Include(c => c.User).FirstOrDefault(a => a.id == id);
        }

        /// <summary>
        /// The list of all products
        /// </summary>
        /// <returns>list of products from db</returns>
        public ICollection<Product> GetProducts()
        {
            return _db.products.OrderBy(a => a.productName).ToList();
        }

        /// <summary>
        /// Check whether the product exists or not by its name
        /// </summary>
        /// <param name="name">The product name</param>
        /// <returns>The product</returns>
        public bool ProductExists(string name)
        {
            bool value = _db.products.Any(a => a.productName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        /// <summary>
        /// Check whether the product exists or not by its id
        /// </summary>
        /// <param name="id">The product id</param>
        /// <returns>bool</returns>
        public bool ProductExists(int id)
        {
            return _db.products.Any(a => a.id == id);
        }

        /// <summary>
        /// The Save method
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        /// <summary>
        /// Method to update the product
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        public bool UpdateProduct(Product product)
        {
            _db.products.Update(product);
            return Save();
        }

        /// <summary>
        /// To add an item to shopping cart
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
       public bool AddItemToShoppingCart(ShoppingCart shoppingCart)
        {
            _db.shoppingCarts.Add(shoppingCart);
            return Save();
        }

        /// <summary>
        /// To update an item in shopping cart
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        public bool UpdateShopingCart(ShoppingCart shoppingCart)
        {
            _db.shoppingCarts.Update(shoppingCart);
            return Save();
        }

        /// <summary>
        /// Method to buy the product
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        public bool BuyProduct(Product product)
        {
            _db.products.Update(product);
            return Save();
        }
    }
}

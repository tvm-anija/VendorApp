using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    /// <summary>
    /// The product repository interface
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Method to get all products
        /// </summary>
        /// <returns></returns>
        ICollection<Product> GetProducts();

        /// <summary>
        /// Method to get individual product
        /// </summary>
        /// <param name="id">The product id</param>
        /// <returns>The product objec</returns>
        Product GetProduct(int id);

        /// <summary>
        /// Check whether the product exists or not based on name
        /// </summary>
        /// <param name="name">The product name</param>
        /// <returns>bool</returns>
        bool ProductExists(string name);

        /// <summary>
        /// Check whether the product exists or not based on id
        /// </summary>
        /// <param name="id">The product id</param>
        /// <returns>bool</returns>
        bool ProductExists(int id);

        /// <summary>
        /// To create a new product
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        bool CreateProduct(Product product);

        /// <summary>
        /// To update a product
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        bool UpdateProduct(Product product);

        /// <summary>
        /// To delete a product
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        bool DeleteProduct(Product product);

        /// <summary>
        /// Method to save a product
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>
        /// To add an item to shopping cart
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        bool AddItemToShoppingCart(ShoppingCart shoppingCart);

        /// <summary>
        /// To update an item in shopping cart
        /// </summary>
        /// <param name="product">The product object</param>
        /// <returns>bool</returns>
        bool UpdateShopingCart(ShoppingCart shoppingCart);
    }
}

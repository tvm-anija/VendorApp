using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VendorWeb.Models
{
    public class ShoppingCart
    {
        /// <summary>
        /// The product id to uniquely identify the product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The select id that is mapped to identify the user
        /// </summary>
        public string ApplicationUserId { get; set; }
        public int MenuItemId { get; set; }

        /// <summary>
        /// The cost of product
        /// </summary>
        [Required]
        public int Cost { get; set; }

        /// <summary>
        /// The name of product
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// The balance amount
        /// </summary>
        public int AmountAvailable { get; set; }
        public int Count { get; set; }

        public ICollection<ShoppingCart> shoppingCarts { get; set; }
        public ICollection<Product> products { get; set; }
        public User User { get; set; }
        public int ItemTotal { get; set; }
    }
}

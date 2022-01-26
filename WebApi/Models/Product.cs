using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    /// <summary>
    /// The product model class
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The product id to uniquely identify a product
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// The seller id which specifies the unique user id
        /// </summary>
        [Required]
        public int sellerId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        /// <summary>
        /// Specifies the cost of product
        /// </summary>
        public int cost { get; set; }

        /// <summary>
        /// Specifies the product name
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// Specifies the balance amount in account
        /// </summary>
        public int amountAvailable { get; set; }
    }
}

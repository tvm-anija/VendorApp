using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTOs
{
    /// <summary>
    /// The product data transfer object model class
    /// </summary>
    public class ProductUpdateDto
    {
        /// <summary>
        /// The product id to uniquely identify the product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The select id that is mapped to identify the user
        /// </summary>
        public int SellerId { get; set; }

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
    }
}

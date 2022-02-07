using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VendorWeb.Models
{
    public class CreateProduct
    {
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

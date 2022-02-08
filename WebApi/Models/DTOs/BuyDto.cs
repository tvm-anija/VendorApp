using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTOs
{
    public class BuyDto
    {
        public ICollection<ShoppingCart> shoppingCarts { get; set; }
        public ICollection<Product> products { get; set; }
        public User User { get; set; }
    }
}

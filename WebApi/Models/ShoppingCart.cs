using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [NotMapped]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [NotMapped]
        [ForeignKey("id")]
        public virtual Product Product { get; set; }
        public int MenuItemId { get; set; }
        public int Count { get; set; }
    }
}

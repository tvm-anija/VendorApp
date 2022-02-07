using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    /// <summary>
    /// The Db context class
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// The Db context
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        /// <summary>
        /// The product DB set
        /// </summary>
        public DbSet<Product> products { get; set; }

        /// <summary>
        /// The users DB set
        /// </summary>
        public DbSet<User> users { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
    }
}

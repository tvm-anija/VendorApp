using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApi.Models
{
    /// <summary>
    /// The user model class
    /// </summary>
    public class User
    {
        /// <summary>
        /// The user Id to uniquely identify the user
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// The user name
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The deposit amount
        /// </summary>
        public int Deposit { get; set; }

        /// <summary>
        /// The user roles
        /// </summary>
        public enum RoleTye {
            /// <summary>
            /// The seller
            /// </summary>
            seller=0,
            /// <summary>
            /// The buyer
            /// </summary>
            buyer=1
        }

        /// <summary>
        /// The role of user
        /// </summary>
        [Required]
        public RoleTye Role { get; set; }

        /// <summary>
        /// The token
        /// </summary>

        [NotMapped]
        public string Token { get; set; }
    }
}

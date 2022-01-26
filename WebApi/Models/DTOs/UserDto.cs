using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WebApi.Models.User;

namespace WebApi.Models.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// The user Id to uniquely identify the user
        /// </summary>
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
        [Required]
        public RoleTye Role { get; set; }
    }
    }

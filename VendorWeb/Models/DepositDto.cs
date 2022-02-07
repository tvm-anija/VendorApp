using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorWeb.Models
{
    public class DepositDto
    {
        /// <summary>
        /// The user Id to uniquely identify the user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The deposit amount
        /// </summary>
        public int Deposit { get; set; }
    }
}
